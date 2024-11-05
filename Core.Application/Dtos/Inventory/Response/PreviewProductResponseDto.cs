using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory.Response
{
    public class PreviewProductResponseDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public int Stock { get; set; }
    }
}
