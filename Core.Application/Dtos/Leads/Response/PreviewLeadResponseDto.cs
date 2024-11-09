﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.Leads.Response
{
    public class PreviewLeadResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int PhoneNumber {  get; set; }

        public string Address {  get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
    }
}
