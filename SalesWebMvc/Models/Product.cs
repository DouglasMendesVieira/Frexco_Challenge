using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Frexco.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        public Product()
        {
        }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddStock(Stock stock)
        {
            Stocks.Add(stock);
        }
    }
}