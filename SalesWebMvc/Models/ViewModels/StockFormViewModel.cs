using System.Collections.Generic;

namespace Frexco.Models.ViewModels
{
    public class StockFormViewModel
    {
        public Stock Stock { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}