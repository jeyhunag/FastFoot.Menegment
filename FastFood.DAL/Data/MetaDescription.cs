using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class MetaDescription :BaseEntity
    {
        public string Description { get; set; }
        public int CitiesId { get; set; }
        public Cities? Cities { get; set; }
    }
}
