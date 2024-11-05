using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory.Response
{
    public class TrackingTypeResponseDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Key { get; set; }
    }
}
