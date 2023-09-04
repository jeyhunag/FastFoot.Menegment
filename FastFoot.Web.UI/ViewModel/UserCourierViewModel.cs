using FastFood.DAL.Data;

namespace FastFoot.Web.UI.ViewModel
{
    public class UserCourierViewModel
    {
        public AppUser AppUser { get; set; }
        public ProfileViewModel Profile { get; set; }
        public IEnumerable<Orders> Orders { get; set; }
    }
}
