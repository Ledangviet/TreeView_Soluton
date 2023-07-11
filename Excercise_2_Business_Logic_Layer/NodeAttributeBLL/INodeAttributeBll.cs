using Excercise_2_Data_Transfer_Object.NodeAttributeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.NodeAttributeBLL
{
    public interface INodeAttributeBll
    {
        public Task<List<GetNodeAttributeModel>> GetAllNodeAttributeAsync();
        public Task<List<GetNodeAttributeModel>> GetNodeAttributeByNodeIdAsync(int nodeId);
        public Task<GetNodeAttributeModel> GetNodeAttributeByIdAsync(int nodeAttributeId);
        public Task<List<GetNodeAttributeModel>> AddNodeAttributeAsync(AddNodeAttributeModel addNodeAttributeModel);
        public Task<GetNodeAttributeModel> UpdateNodeAttributeAsync(UpdateNodeAttributeModel updateNodeAttributeModel);
        public Task<List<GetNodeAttributeModel>> DeleteNodeAttributeByIdAsync(int nodeAttributeId);

        public Task<List<GetNodeAttributeModel>> DeleteNodeAttributeByNodeIdAsync(int nodeId);

        

    }
}
