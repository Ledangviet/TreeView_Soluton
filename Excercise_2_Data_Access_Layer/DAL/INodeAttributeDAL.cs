using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.NodeAttributeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Access_Layer.DAL
{
    public interface INodeAttributeDAL
    {
        public Task<List<NodeAttribute>> GetAllNodeAttributeAsync();
        public Task<NodeAttribute> GetNodeAttributeByIdAsync(int id);
        public Task<bool> AddNodeAttributeAsync(NodeAttribute nodeAttribute);
        public Task<bool> DeleteNodeAttributeAsync(NodeAttribute nodeAttribute);
        public Task<bool> UpdateNodeAttributeAsync(NodeAttribute nodeAttribute);
    }
}
