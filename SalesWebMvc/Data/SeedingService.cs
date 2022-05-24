using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frexco.Models;

namespace Frexco.Data
{
    public class SeedingService
    {
        private FrexcoContext _context;

        public SeedingService(FrexcoContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Product.Any() ||
                _context.Stock.Any())
            {
                return; // DB has been seeded
            }

            Product p1 = new Product(1, "Shampoo");
            Product p2 = new Product(2, "Conditioner");
            Product p3 = new Product(3, "Soap");
            Product p4 = new Product(4, "Milk");
            Product p5 = new Product(5, "Cookie");

            Stock s1 = new Stock(1, "Shampoo Pantene", 100.0, p1);
            Stock s2 = new Stock(2, "Conditioner Seda", 35.0, p2);
            Stock s3 = new Stock(3, "Soap Dove", 22.0, p3);
            Stock s4 = new Stock(4, "Soap Scala", 30.0, p3);
            Stock s5 = new Stock(5, "Milk Piracanjuba", 40.0, p4);
            Stock s6 = new Stock(6, "Cookie Marilan", 300.0, p5);

            _context.Product.AddRange(p1, p2, p3, p4, p5);

            _context.Stock.AddRange(s1, s2, s3, s4, s5, s6);

            _context.SaveChanges();
        }
    }
}