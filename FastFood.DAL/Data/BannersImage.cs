using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class BannersImage :BaseEntity
    {
        public string ImagePath { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; }
        public bool IsMain { get; set; }
    }
}
