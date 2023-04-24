
using Microsoft.EntityFrameworkCore;
using NodeTree.Data.Models;
using NodeTree.Infrastructure.SystemExceptions;

namespace NodeTree.Infrastructure.NodeContext
{
   public class AppDBContext:DbContext
    {
        public AppDBContext()
        {
            
        }
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=NodeDB;trusted_connection=true;TrustServerCertificate=True;");
        }

        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<ExceptionLog> Exceptions { get; set; }

    }
}
