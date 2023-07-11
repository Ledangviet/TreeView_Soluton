using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.NodeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.Node_BLL
{
    
    public interface INodeBll
    {
        public Task<List<GetNodeModel>> GetAllNodeAsync();
        public Task<GetNodeModel> GetNodeByIdAsync(int id);
        public Task<List<GetNodeModel>> DeleteNodeAsync(int id);
        public Task<UpdateNodeResponseModel> UpdateNodeAsync(UpdateNodeModel nodemodel);
        public Task<AddNodeResultModel> AddNodeAsync(AddNodeModel nodemodel);
        public Task<string> Test();
    }
}
