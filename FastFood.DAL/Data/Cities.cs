using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Cities : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<Restaurants>? Restaurants { get; set; }
        public virtual ICollection<Courier>? Couriers { get; set; }
        public virtual ICollection<Orders>? Orders { get; set; }
        public virtual ICollection<SiteInfo>? SiteInfos { get; set; }
        public virtual ICollection<MetaDescription>? MetaDescriptions { get; set; }
        public virtual ICollection<MetaKeyword>? MetaKeywords { get; set; }

    }
}
