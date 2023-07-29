using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{
    public class SiteInfo : BaseEntity ,IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string FavIcon { get; set; }

        [Required(ErrorMessage = "The name must be written")]
        [MinLength(3, ErrorMessage = "Site name at least must contain 3 letters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The number must be written")]
        public string Number { get; set; }

        [Required(ErrorMessage = "The email must be written")]
        [EmailAddress(ErrorMessage = "Incorrect email format (example.com)")]
        public string Email { get; set; }
        public string Address { get; set; }
        public int CitiesId { get; set; }
        public Cities? Cities { get; set; }
    }
}
