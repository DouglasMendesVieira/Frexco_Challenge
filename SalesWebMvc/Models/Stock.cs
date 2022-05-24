using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Frexco.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(1.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Quantity in Stock")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Qtd { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public Stock()
        {
        }

        public Stock(int id, string name, double qtd, Product department)
        {
            Id = id;
            Name = name;
            Qtd = qtd;
            Product = department;
        }
    }
}