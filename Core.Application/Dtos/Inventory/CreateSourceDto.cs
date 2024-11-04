using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory
{
    public class CreateSourceDto
    {
        public int TrackingTypeId { get; set; }
        public string TrackingCode { get; set; }
        public DateOnly? Arrival { get; set; }
        public double Weight { get; set; }
        public double PoundsPrice { get; set; }
        public double Finished { get; set; }
    }
}
