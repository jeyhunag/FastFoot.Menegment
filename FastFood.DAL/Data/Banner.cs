using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class Banner : BaseEntity
    {
        public string ImagePath { get; set; }
        public ICollection<BannersImage>? BannersImages { get; set; }

        [NotMapped]
        [Timestamp]
        public ImageItemFormModel[]? Files { get; set; }

    }
}
