using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Taxes
{
    public class Product_Tax : AuditableBase
    {
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }

        public int TaxId { get; set; }
        public Tax Tax { get; set; }
    }
}
