using FastFood.DAL.Data;

namespace FastFoot.Web.UI.ViewModel
{
    public class CampaignBannerViewModel
    {
        public IEnumerable<Campaign> Campaigns { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
    }
}
