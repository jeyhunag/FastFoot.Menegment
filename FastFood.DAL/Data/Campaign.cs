using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Campaign : BaseEntity
    {
        public string ImagePath { get; set; }
        public int? RestaurantsId { get; set; }
        public Restaurants Restaurants { get; set; }
        public int? FoodsId { get; set; }
        public Foods Foods { get; set; }
    }
}
