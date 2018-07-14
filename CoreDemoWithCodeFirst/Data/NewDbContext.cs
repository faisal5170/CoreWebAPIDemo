using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreDemoWithCodeFirst.Models
{
    public class NewDbContext : DbContext
    {
        public NewDbContext (DbContextOptions<NewDbContext> options)
            : base(options)
        {
        }

        public DbSet<CoreDemoWithCodeFirst.Models.MyFirstTables> MyFirstTables { get; set; }
    }
}
