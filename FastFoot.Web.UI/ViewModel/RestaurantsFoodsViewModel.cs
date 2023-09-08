using FastFood.DAL.Data;

namespace FastFoot.Web.UI.ViewModel
{
    public class RestaurantsFoodsViewModel
    {
        public IEnumerable<Restaurants> Restaurants { get; set; }
        public IEnumerable<Foods> Foods { get; set; }

    }
}
