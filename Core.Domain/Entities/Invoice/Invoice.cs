using Core.Domain.Entities.Auditable;
using Core.Domain.Entities.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Invoice
{
    public class Invoice : AuditableBase
    {
        public int LeadId { get; set; }
        public Leads.Leads Leads { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public string SellerId { get; set; }
        public double Delivery { get; set; }
        public double Discount { get; set; }
        public double TotalRaw { get; set; }
        public double Total { get; set; }
        public bool Finished { get; set; }

        public ICollection<Product_Invoice> Product_Invoices { get; set; }
    }
}
