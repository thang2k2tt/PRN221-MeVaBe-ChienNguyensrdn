﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.DTOs
{
    public class CreateAddressDTO
    {

        public int? UserId { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Address { get; set; }
    }
}