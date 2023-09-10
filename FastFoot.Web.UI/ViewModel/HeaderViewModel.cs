using FastFood.DAL.Data;

namespace FastFoot.Web.UI.ViewModel
{
    public class HeaderViewModel
    {
        public SiteInfo Site { get; set; }
        public IEnumerable<SocialLink> socialLinks { get; set; }
    }
}
