﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class GetMemberDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public float Hours { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }

    }
}
