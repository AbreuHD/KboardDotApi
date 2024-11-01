using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Auditable
{
    public class AuditableBase
    {
        public int ID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedby { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
