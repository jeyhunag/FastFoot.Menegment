using FastFood.DAL.Data;

namespace FastFoot.Web.UI.ViewModel
{
    public class UserRestaurantsViewModel
    {
       public AppUser AppUser { get; set; }
        public Restaurants Restaurants { get; set; }
        public IEnumerable<Foods> Foods { get; set; }
        public ProfileViewModel Profile { get; set; }
    }
}
