using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NodeTree.Application.Interface;
using NodeTree.Data.Models;
using NodeTree.Infrastructure.NodeContext;
using Polly;

namespace NodeTree.Infrastructure.Repository
{
    public class NodeTreeRepository : INodeTree
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        public NodeTreeRepository(AppDBContext db,IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<TreeNode>> GetTreeNodes(int rootId)
        {
         
                var rootNode = _db.TreeNodes
                    .Include(x => x.Children)
                    .SingleOrDefault(x => x.Id == rootId);

                if (rootNode != null)
                {
                    return await GetChildrenNodes(rootNode);
                }
            

            return null;
        }

        public async Task<List<TreeNode>> GetChildrenNodes(TreeNode parentNode)
        {
            var children = new List<TreeNode>();

            if (parentNode.Children != null)
            {
                foreach (var child in parentNode.Children)
                {
                    children.Add(child);
                    children.AddRange(await GetChildrenNodes(child));
                }
            }

            return children;
        }

        public async Task<TreeNode> AddTreeNode(TreeNodeDto node)
        {
            try
            {
                var treenode=_mapper.Map<TreeNode>(node);
                if (treenode != null)
                {
                    if (treenode.ParentId == 0)
                    {
                        treenode.ParentId =null;
                    }
                }
                var xx = await _db.TreeNodes.AddAsync(treenode);
                await _db.SaveChangesAsync();
                return xx.Entity;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}", ex);
            }
   
        }

        public async Task<TreeNode> GetTreeNode(int Id)
        {
            try
            {
                var rootNode = _db.TreeNodes
                              .Include(x => x.Children)
                              .SingleOrDefault(x => x.Id == Id);
                return rootNode;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}", ex);
            }


        }

        public async Task DeleteNode(int id)
        {
            var node = await GetTreeNode(id); 
            if (node != null)
            {
                _db.TreeNodes.Remove(node);
                await _db.SaveChangesAsync();
            }
          
        }
    }
}
