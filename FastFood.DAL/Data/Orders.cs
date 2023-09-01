using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Orders : BaseEntity
    {
        public string OrderNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Bina { get; set; }
        public string Number { get; set; }
        public string MenzilNo { get; set; }
        public string TotalPrice { get; set; }
        public string Email { get; set; }
        public int RestaurantsId { get; set; }
        public Restaurants Restaurants { get; set; }
        public int FoodsId { get; set; }
        public Foods Foods { get; set; }
        public int CitiesId { get; set; }
        public Cities Cities { get; set; }
        public int CourierId { get; set; }
        public Courier Courier { get; set; }
        public string? QR { get; set; }
    }
}
