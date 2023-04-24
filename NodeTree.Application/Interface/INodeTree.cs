using NodeTree.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeTree.Application.Interface
{
    public interface INodeTree
    {
        public Task<TreeNode> AddTreeNode(TreeNodeDto node);
        public Task<TreeNode> GetTreeNode(int Id);
        public Task<List<TreeNode>> GetTreeNodes(int rootId);
        public Task<List<TreeNode>> GetChildrenNodes(TreeNode parentNode);
        public Task DeleteNode(int id);
    }
}
