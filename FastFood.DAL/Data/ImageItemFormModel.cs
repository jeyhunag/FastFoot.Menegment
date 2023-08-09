using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.DAL.Data
{

    public class ImageItemFormModel
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public string TempPath { get; set; }
        public IFormFile? File { get; set; }
    }
}