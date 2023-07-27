using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Courier : BaseEntity, IBaseEntity
    {
        public string Name { get ; set ; }
        public int CitiesId { get ; set ; }
        public Cities Cities { get ; set ; }
        public virtual ICollection<Orders>? Orders { get; set; }
    }
}
