using Excercise_2_Data_Access_Layer.Data.DbContexts;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.NodeAttributeDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public class NodeAttributeDAL : INodeAttributeDAL
    {
        private readonly NodeDbContext _dbContext;
        public NodeAttributeDAL(NodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddNodeAttributeAsync(NodeAttribute nodeAttribute)
        {
            try
            {
                _dbContext.Attributes.Add(nodeAttribute);
                var result = await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteNodeAttributeAsync(NodeAttribute nodeAttribute)
        {
            try
            {
                _dbContext.Attributes.Remove(nodeAttribute);
                var result = await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<NodeAttribute> GetNodeAttributeByIdAsync(int id)
        {
            try
            {
                var nodeAtt =
                    await _dbContext.Attributes.FromSql($"SELECT * FROM Attributes where id = {id} AND IsDeleted <> 1").FirstOrDefaultAsync();
                return nodeAtt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NodeAttribute>> GetAllNodeAttributeAsync()
        {
            try
            {
                List<NodeAttribute> nodeAttributes = await _dbContext.Attributes.Where(a => a.IsDeleted == false).ToListAsync();
                return nodeAttributes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateNodeAttributeAsync(NodeAttribute nodeAttribute)
        {
            try
            {
                _dbContext.Attributes.Update(nodeAttribute);
                var result = await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
