using AutoMapper;
using NodeTree.Data.Models;

namespace NodeTree.Application.Profiles
{
    public class TreeNodeProfile:Profile
    {
        public TreeNodeProfile()
        {
            CreateMap<TreeNodeDto,TreeNode>();
        }
    }
}
