using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Price { get; set; }
    }
}
