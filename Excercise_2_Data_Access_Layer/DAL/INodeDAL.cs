using Excercise_2_Data_Access_Layer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public interface INodeDAL
    {
        public Task<List<Node>> GetAllNodeAsync();
        public Task<Node> GetNodeByIdAsync(int id);
        public Task<bool> DeleteNodeAsync(int id);
        public Task<bool> UpdateNode(Node node);
        public Task<bool> AddNode(Node node);
        public Task<Node> GetLastNodeAsync();
    }
}
