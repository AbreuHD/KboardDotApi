using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Source
{
    public class TrackingType : AuditableBase
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Key { get; set; }

        public ICollection<Source> Sources { get; set; }
    }
}
