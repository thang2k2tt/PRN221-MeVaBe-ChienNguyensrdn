﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_MeVaBe_Repo.DTOs
{
    public class CreateAccountDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public DateOnly? Dob { get; set; }

        public string Phone { get; set; }
    }
}
