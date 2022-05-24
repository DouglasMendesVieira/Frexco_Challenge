using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Frexco.Models
{
    public class FrexcoContext : DbContext
    {
        public FrexcoContext(DbContextOptions<FrexcoContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }
}