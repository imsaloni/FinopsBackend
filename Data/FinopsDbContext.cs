using Finops.Models;
using Microsoft.EntityFrameworkCore;
using resource.Models;
using ResourcesWebApi.Models;

namespace Finops.Data
{
    public class FinopsDbContext:DbContext
    {
        public FinopsDbContext(DbContextOptions options) : base (options)
            {       
        }
             public DbSet<ResourcesData> ResourcesDatas { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Resource> Resources { get; set; }

        
    
    }
}

