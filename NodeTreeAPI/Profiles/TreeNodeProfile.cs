using AutoMapper;
using NodeTree.API.Dtos;
using NodeTree.Data.Models;

namespace NodeTree.API.Profiles
{
    public class TreeNodeProfile:Profile
    {
        public TreeNodeProfile()
        {
            CreateMap<TreeNodeAdd, TreeNodeDto>();
        }
    }
}
