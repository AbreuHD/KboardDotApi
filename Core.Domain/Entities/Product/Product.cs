using Core.Domain.Entities.Auditable;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Invoice;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Product
{
    public class Product : AuditableBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public int Stock { get; set; }

        public ICollection<Product_Category> Product_Categories { get; set; }
        public ICollection<Characteristic_Product> Characteristic_Products { get; set; }
        public ICollection<Product_invoice> Product_Invoices { get; set; }
        public ICollection<Source_Product> Source_Product { get; set; }
        public ICollection<Product_Tax> Product_Tax { get; set; }
    }
}
