using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Generic
{
    public class GenericApiResponse<Dto>
    {
        public Dto Payload { get; set; }
        public bool Success { get; set; } = true;
        public int Statuscode { get; set; }
        public string Message { get; set; }
    }
}
