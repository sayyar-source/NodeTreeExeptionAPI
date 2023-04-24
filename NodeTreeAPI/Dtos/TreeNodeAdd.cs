namespace NodeTree.API.Dtos
{
    public class TreeNodeAdd
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public int? ParentId { get; set; } = null;
    }
}
