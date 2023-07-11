using AutoMapper;
using Excercise_2_Data_Access_Layer.DAL;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.NodeAttributeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.NodeAttributeBLL
{
    public class NodeAttributeBll : INodeAttributeBll
    {
        private readonly INodeDAL _nodeDAL;
        private readonly INodeAttributeDAL _nodeAttributeDAL;
        private readonly IMapper _mapper;
        public NodeAttributeBll(INodeAttributeDAL nodeAttributeDAL, IMapper mapper, INodeDAL nodeDAL)
        {
            _nodeAttributeDAL = nodeAttributeDAL;
            _mapper = mapper;
            _nodeDAL = nodeDAL;
        }

        /// <summary>
        /// Add attribute to db
        /// </summary>
        /// <param name="addNodeAttributeModel"></param>
        /// <returns>List of all Attribute</returns>
        public async Task<List<GetNodeAttributeModel>> AddNodeAttributeAsync(AddNodeAttributeModel addNodeAttributeModel)
        {
            try
            {
                NodeAttribute nodeAttribute = _mapper.Map<NodeAttribute>(addNodeAttributeModel);
                var result = await _nodeAttributeDAL.AddNodeAttributeAsync(nodeAttribute);
                List<GetNodeAttributeModel> nodes =
                    _mapper.Map<List<GetNodeAttributeModel>>(await _nodeAttributeDAL.GetAllNodeAttributeAsync());
                return nodes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a nodeAttribute
        /// </summary>
        /// <param name="nodeAttributeId"></param>
        /// <returns>List of all nodeAttribute</returns>
        public async Task<List<GetNodeAttributeModel>> DeleteNodeAttributeByIdAsync(int nodeAttributeId)
        {
            try
            {
                NodeAttribute node = await _nodeAttributeDAL.GetNodeAttributeByIdAsync(nodeAttributeId);
                if (node != null)
                {
                    var result = await _nodeAttributeDAL.DeleteNodeAttributeAsync(node);
                    return _mapper.Map<List<GetNodeAttributeModel>>(await _nodeAttributeDAL.GetAllNodeAttributeAsync());
                }
                return null;


            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get All nodeAttribute
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetNodeAttributeModel>> GetAllNodeAttributeAsync()
        {
            try
            {
                List<GetNodeAttributeModel> nodeAttributeModels =
                    _mapper.Map<List<GetNodeAttributeModel>>(await _nodeAttributeDAL.GetAllNodeAttributeAsync());
                return nodeAttributeModels;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all node attribute of a node
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<List<GetNodeAttributeModel>> GetNodeAttributeByNodeIdAsync(int nodeId)
        {
            try
            {
                List<NodeAttribute> attributes = await _nodeAttributeDAL.GetAllNodeAttributeAsync();
                List<NodeAttribute> nodeAttributes = attributes.Where(a => a.NodeId == nodeId).ToList();
                return _mapper.Map<List<GetNodeAttributeModel>>(nodeAttributes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get an nodeAttribute
        /// </summary>
        /// <param name="nodeAttributeId"></param>
        /// <returns></returns>
        public async Task<GetNodeAttributeModel> GetNodeAttributeByIdAsync(int nodeAttributeId)
        {
            try
            {
                NodeAttribute nodeAttribute = await _nodeAttributeDAL.GetNodeAttributeByIdAsync(nodeAttributeId);
                GetNodeAttributeModel nodeAttributeModel = _mapper.Map<GetNodeAttributeModel>(nodeAttribute);
                return nodeAttributeModel;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update a nodeAttribute
        /// </summary>
        /// <param name="updateNodeAttributeModel"></param>
        /// <returns>nodeAttribute after update</returns>
        public async Task<GetNodeAttributeModel> UpdateNodeAttributeAsync(UpdateNodeAttributeModel updateNodeAttributeModel)
        {
            try
            {
                NodeAttribute nodeAttribute = _mapper.Map<NodeAttribute>(updateNodeAttributeModel);
                await _nodeAttributeDAL.UpdateNodeAttributeAsync(nodeAttribute);
                GetNodeAttributeModel getNodeAttributeModel =
                    _mapper.Map<GetNodeAttributeModel>(await _nodeAttributeDAL.GetNodeAttributeByIdAsync(nodeAttribute.Id));
                return getNodeAttributeModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete all nodeAttribute of a node
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<List<GetNodeAttributeModel>> DeleteNodeAttributeByNodeIdAsync(int nodeId)
        {
            try
            {
                List<NodeAttribute> attributes = await _nodeAttributeDAL.GetAllNodeAttributeAsync();
                List<NodeAttribute> nodeAttributes = attributes.Where(a => a.NodeId == nodeId).ToList();
                foreach(NodeAttribute nodeAttribute in nodeAttributes)
                {
                    await _nodeAttributeDAL.DeleteNodeAttributeAsync(nodeAttribute);
                }
                var list = this.GetNodeAttributeByNodeIdAsync(nodeId);
                if (list == null)
                    return _mapper.Map<List<GetNodeAttributeModel>>(await _nodeAttributeDAL.GetAllNodeAttributeAsync());
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
