using Excercise_2_Data_Access_Layer.Data.DbContexts;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public class NodeDAL : INodeDAL
    {
        private readonly string connectionString = "Server=.;Database=Excercise2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        private readonly NodeDbContext _nodeDbContext;
        public NodeDAL(NodeDbContext nodeDbContext)
        {
            _nodeDbContext = nodeDbContext;
        }
        /// <summary>
        /// Add a node to database through DbContext
        /// </summary>
        /// <param name="node"></param>
        /// <returns> new node </returns>
        public async Task<bool> AddNode(Node node)
        {
            try
            {
                _nodeDbContext.Nodes.Add(node);
                var result = await _nodeDbContext.SaveChangesAsync();
                if (result == 1)
                    return true;
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a Node
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNodeAsync(int id)
        {
            try
            {
                var result = 0;
                Node node = await GetNodeByIdAsync(id);
                if (node != null)
                {
                    //var result = await _nodeDbContext.Database.ExecuteSqlAsync($"Delete from Nodes where Id = {id}");
                    //_nodeDbContext.Nodes.Remove(node);

                    var sqlCommand = $"EXECUTE DeleteNode @Id";

                    List<Node> nodes = new();

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(sqlCommand, connection))
                        {
                            command.Parameters.AddWithValue("@Id", id);
                            result = command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }                   
                    if (result == 1) return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get all Node from database through DbContext
        /// </summary>
        /// <returns> node list "List<Node>" </Node></returns>
        public async Task<List<Node>> GetAllNodeAsync()
        {
            try
            {
                var sqlCommand = $"select * from Nodes where isdeleted <> 1";

                List<Node> nodes = new();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(sqlCommand, connection))
                    {
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var node = new Node
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ParrentId = reader.GetInt32(2),
                                NodeType = reader.GetInt32(3) == 0 ? NodeType.Folder : NodeType.File,
                                Owner = reader.GetString(4),
                                SubmissionDate = reader.GetDateTime(5),
                                IsDeleted = reader.GetBoolean(6),
                            };
                            nodes.Add(node);
                        }
                        reader.Close();
                    }

                    connection.Close();
                }


                var nodess = await _nodeDbContext.Nodes.FromSql($"select * from Nodes where isdeleted <> 1").ToListAsync();
                return nodes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get a nodeby Id through DbContext
        /// </summary>
        /// <param name="id"></param>
        /// <returns> a Node</returns>
        public async Task<Node> GetNodeByIdAsync(int id)
        {
            try
            {
                var node = _nodeDbContext.Nodes.FromSql($"EXECUTE GetNodeById {id}").AsEnumerable().FirstOrDefault();
                //var node = await _nodeDbContext.Nodes.FromSql($"Select * from Nodes where id = {id} and isdeleted <> 1").FirstOrDefaultAsync();
                return node;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update Node through DbContext
        /// </summary>
        /// <param name="node"></param>
        /// <returns> Updated node </returns>
        public async Task<bool> UpdateNode(Node node)
        {
            try
            {
                _nodeDbContext.Nodes.Update(node);
                var result = await _nodeDbContext.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the lastest node
        /// </summary>
        /// <returns></returns>
        public async Task<Node> GetLastNodeAsync()
        {
            try
            {
                var result = await _nodeDbContext.Nodes.OrderBy(e => e.Id).LastAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
