using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Excercise_2_Data_Access_Layer.DAL;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Excercise_2_Data_Transfer_Object.NodeDTO;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Business_Logic_Layer.Node_BLL
{
    public class NodeBll : INodeBll
    {
        private readonly INodeDAL _nodeDAL;
        private readonly IMapper _mapper;
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NodeBll(INodeDAL nodeDAL, IMapper mapper)
        {
            _nodeDAL = nodeDAL;
            _mapper = mapper;
        }

        /// <summary>
        /// Build a treeview structure of NodeModel
        /// </summary>
        /// <param name="getNodeModels"></param>
        /// <param name="parrentId"></param>
        /// <returns> list root GetNodeModel</returns>
        public List<GetNodeModel> BuildTree(List<GetNodeModel> getNodeModels , int parrentId)
        {
            List<GetNodeModel> child = new();
            foreach (var node in getNodeModels)
            {
                if (node.ParrentId == parrentId)
                {
                    node.Childs = BuildTree(getNodeModels, node.Id);
                    child.Add(node);
                }
            }
            return child;

            //for (int i = 0; i < getNodeModels.Count; i++)
            //{
            //    getNodeModels[i].Childs = getNodeModels.Where(m => m.ParrentId == getNodeModels[i].Id).ToList();
            //}
            //var roots = getNodeModels.Where(m => m.ParrentId == 0).ToList();
            //return roots;
        }
     

        /// <summary>
        /// get all Node from dal and map to GetNodeModel
        /// </summary>
        /// <returns> list GetNodeModel</returns>
        public async Task<List<GetNodeModel>> GetAllNodeAsync()
        {
            try
            {
                List<Node> nodes = await _nodeDAL.GetAllNodeAsync();
                List<GetNodeModel> nodemodels = _mapper.Map<List<GetNodeModel>>(nodes.OrderBy(n => n.ParrentId));
                var models = BuildTree(nodemodels, 0);
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get node by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> a GetNodeModel</returns>
        public async Task<GetNodeModel> GetNodeByIdAsync(int id)
        {
            try
            {
                Node node = await _nodeDAL.GetNodeByIdAsync(id);
                GetNodeModel nodemodel = _mapper.Map<GetNodeModel>(node);
                return nodemodel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update node through nodeDAL update method
        /// </summary>
        /// <param name="nodemodel"></param>
        /// <returns>a  GetNodeModel </returns>
        public async Task<UpdateNodeResponseModel> UpdateNodeAsync(UpdateNodeModel nodemodel)
        {
            try
            {
                UpdateNodeResponseModel responseModel = new();
                Node node1 = _mapper.Map<Node>(nodemodel);
                var node2 = await _nodeDAL.GetNodeByIdAsync(nodemodel.Id);
                if(node2.NodeType == Excercise_2_Data_Transfer_Object.Enums.NodeType.Folder &&
                    node1.NodeType == Excercise_2_Data_Transfer_Object.Enums.NodeType.File)
                {
                    responseModel.Status = "Folder cannot update to file!";
                    return responseModel;
                }    
                var result = await _nodeDAL.UpdateNode(node1);
                if (result == true)
                {
                    responseModel.Succeed = true;
                    GetNodeModel getnodemodel = _mapper.Map<GetNodeModel>(await _nodeDAL.GetNodeByIdAsync(nodemodel.Id));
                    responseModel.NodeModel = getnodemodel;
                    responseModel.Status = "Update succeed!";
                    return responseModel;
                }
                else return responseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// delete node by id through nodeDAL delete method
        /// </summary>
        /// <param name="id"></param>
        /// <returns> </returns>
        public async Task<List<GetNodeModel>> DeleteNodeAsync(int id)
        {
            try
            {
                var result = await _nodeDAL.DeleteNodeAsync(id);
                if (result == true)
                {
                    List<GetNodeModel> nodemodels = _mapper.Map<List<GetNodeModel>>(await _nodeDAL.GetAllNodeAsync());
                    return nodemodels;
                }
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add a node to db through nodeDAL
        /// </summary>
        /// <param name="nodemodel"></param>
        /// <returns></returns>
        public async Task<AddNodeResultModel> AddNodeAsync(AddNodeModel nodemodel)
        {
            try
            {
                AddNodeResultModel resultModel = new();
                Node node = _mapper.Map<Node>(nodemodel);
                var result = await _nodeDAL.AddNode(node);
                if (result == true)
                {
                    resultModel.Succeed = true;
                    GetNodeModel newnode = _mapper.Map<GetNodeModel>(await _nodeDAL.GetLastNodeAsync());
                    resultModel.Nodes = newnode;
                    resultModel.Status = "Add succeed!";
                    return resultModel;
                }
                resultModel.Status = "Add failed!";
                return resultModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Test()
        {

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _log.Error("From Node DAL");
                throw;
            }


        }
    }
}
