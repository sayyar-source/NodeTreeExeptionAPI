using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NodeTree.API.Dtos;
using NodeTree.Data.Models;
using AutoMapper;
using NodeTree.Application.Interface;
using NodeTree.Infrastructure.SystemExceptions;

namespace NodeTreeAPI.Controllers
{
    [Route("api/trees")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly INodeTree _nodeTree;
        private readonly IMapper _mapper; 
        public TreeController(INodeTree nodeTree, IMapper mapper)
        {
            _nodeTree = nodeTree;
            _mapper = mapper;
        }

        // GET: api/trees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeNode>>> GetTreeNodes([FromQuery]int rootid)
        {
            try
            {
                var result = await _nodeTree.GetTreeNodes(rootid);
                return Ok(result);
            }
            catch (Exception)
            {
                throw new SecureException("Node not found");
            }
        }

        // GET: api/trees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreeNode>> GetTreeNode(int id)
        {
            try
            {
                var treeNode = await _nodeTree.GetTreeNode(id);

                if (treeNode == null)
                {
                    return NotFound();
                }

                return treeNode;
            }
            catch (Exception)
            {
                throw new SecureException("Node not found");
            }
        }

        // POST: api/trees
        [HttpPost]
        public async Task<ActionResult<TreeNode>> PostTreeNode([FromBody]TreeNodeAdd treeNode)
        {
            try
            {
                var node = _mapper.Map<TreeNodeDto>(treeNode);
                var result = await _nodeTree.AddTreeNode(node);
                return Ok(result);
            }
            catch (Exception)
            {
                throw new SecureException("An error for adding node");
            }
        }

        // DELETE: api/trees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreeNode(int id)
        {
            var childNode = await _nodeTree.GetTreeNode(id);
            if(childNode==null)
            {
                throw new SecureException("NotFound");
            }
            if (childNode.Children.Count>0)
            {
                throw new SecureException("You have to delete all children nodes first");
            }
            else
            {
                _nodeTree.DeleteNode(id);
            }
            return NoContent();
        }
    }
}
