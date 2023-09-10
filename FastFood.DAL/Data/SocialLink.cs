using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class SocialLink:BaseEntity
    {
        public string? Icon { get; set; }
        public string? Link { get; set; }
    }
}
