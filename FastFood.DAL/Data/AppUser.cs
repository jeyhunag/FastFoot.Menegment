﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class AppUser: IdentityUser
    {
        public string? Fincode { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Img { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public virtual ICollection<Restaurants>? Restaurants { get; set; }
    }
}
