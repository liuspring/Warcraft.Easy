﻿using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Nodes.Dto;

namespace TaskManager.Nodes
{
    public class NodeAppService : TaskManagerAppServiceBase, INodeAppService
    {
        private readonly IRepository<Node, int> _nodeRepository;

        public NodeAppService(IRepository<Node, int> nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public int Create(CreateNodeInput input)
        {
            var node = Node.Create(input.NodeName, input.NodeIp);
            _nodeRepository.Insert(node);
            return node.Id;
        }

        public List<NodeListOutput> GetList(NodeListInput input)
        {
            var nodes = _nodeRepository
                .GetAllList()
                .OrderBy(a => a.Id)
                .Skip(input.iDisplayStart)
                .Take(input.iDisplayLength);
            return nodes.MapTo<List<NodeListOutput>>();

        }

        public int GetListTotal(NodeListInput input)
        {
            return _nodeRepository.GetAllList().Count;
        }

        public List<Node> GetAllList()
        {
            return _nodeRepository.GetAllList();
        }
    }
}
