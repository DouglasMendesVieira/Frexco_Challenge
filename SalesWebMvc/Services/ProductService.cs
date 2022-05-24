using Frexco.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Frexco.Services
{
    public class ProductService
    {
        private readonly FrexcoContext _context;

        public ProductService(FrexcoContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> FindAllAsync()
        {
            return await _context.Product.OrderBy(x => x.Name).ToListAsync();
        }
    }
}