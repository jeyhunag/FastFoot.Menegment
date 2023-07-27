using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Restaurants : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Image { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Bina { get; set; }
        public string? Email { get; set; }
        public string? Number { get; set; }
        public string? MenzilNo { get; set; }
        public int CitiesId { get; set; }
        public Cities Cities { get; set; }

        public virtual ICollection<Foods>? foods { get; set; }
        public virtual ICollection<Orders>? Orders { get; set; }

    }
}
