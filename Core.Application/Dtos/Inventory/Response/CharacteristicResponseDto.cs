using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Inventory.Response
{
    public class CharacteristicResponseDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
