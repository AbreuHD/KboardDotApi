using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Source
{
    public class Source_Product : AuditableBase
    {
        public int SourceId { get; set; }
        public Source Source { get; set; }

        public int ProductId { get; set; }
        public Product.Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
