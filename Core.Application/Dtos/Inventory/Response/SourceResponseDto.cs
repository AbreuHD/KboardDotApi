using Core.Domain.Entities.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory.Response
{
    public class SourceResponseDto
    {
        public int ID { get; set; }
        //public int TrackingTypeId { get; set; }
        //public TrackingType TrackingType { get; set; } Modificar luego

        public string TrackingCode { get; set; }
        public DateOnly? Arrival { get; set; }
        public double Weight { get; set; }
        public double PoundsPrice { get; set; }
        public double Finished { get; set; }
    }
}
