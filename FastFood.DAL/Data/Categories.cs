using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Categories : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } //duzeltmek
        public virtual ICollection<Foods>? Foods { get; set; }
        

    }
}
