using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Generic
{
    public class GenericApiResponse<Dto>
    {
        public Dto Payload { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }

        [JsonIgnore]
        public int Statuscode { get; set; }
    }
}
