using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Invoice
{
    public class PaymentType : AuditableBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
