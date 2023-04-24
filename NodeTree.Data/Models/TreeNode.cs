using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NodeTree.Data.Models
{
    public class TreeNode
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public byte? Age { get; set; }
        public int? ParentId { get; set; }
        public  TreeNode Parent { get; set; }
        [JsonIgnore]
        public  ICollection<TreeNode> Children { get; set; }
    }
}
