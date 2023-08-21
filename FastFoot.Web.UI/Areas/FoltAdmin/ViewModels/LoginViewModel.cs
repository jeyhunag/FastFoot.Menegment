using System.ComponentModel.DataAnnotations;

namespace FastFoot.Web.UI.Areas.FoltAdmin.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool RememberMe { get; set; }
    }
}
