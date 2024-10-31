using Core.Domain.Entities.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Source
{
    public class Source : AuditableBase
    {
        public int TrackingTypeId { get; set; }
        public TrackingType TrackingType { get; set; }

        public string TrackingCode { get; set; }
        public DateOnly? Arrival { get; set; }
        public double Weight { get; set; }
        public double PoundsPrice { get; set; }
        public double Finished { get; set; }

        public ICollection<Source_Product> Source_Products { get; set; }
    }
}
