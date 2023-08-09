using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Foods: BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int RestaurantsId { get; set; }
        public Restaurants Restaurants { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }
        public virtual ICollection<Orders>? Orders { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }
        public int Discount { get; set; }
        public string? QR { get; set; }

        public ICollection<ProductImages>? ProductImages { get; set; }

        [NotMapped]
        [Timestamp]
        public ImageItemFormModel[]? Files { get; set; }
    }
}
