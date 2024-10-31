using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Taxes
{
    public class Tax : AuditableBase
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        
        public Product_Tax Product_Tax { get; set; }
    }
}
