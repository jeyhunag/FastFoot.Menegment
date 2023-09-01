using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
