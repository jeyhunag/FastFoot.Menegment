using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class RestaurantsImage :BaseEntity
    {
        public string ImagePath { get; set; }
        public int RestaurantsId { get; set; }
        public Restaurants  Restaurants { get; set; }
        public bool IsMain { get; set; }
    }
}
