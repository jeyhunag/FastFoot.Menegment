﻿using System.ComponentModel.DataAnnotations;

namespace FastFoot.Web.UI.Areas.FoltAdmin.ViewModels
{
    public class UserViewModel
    {
        public string? Fincode { get; set; }
        public string Id { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Img { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }

    }
}
