using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Models
{
    public class AddProductRequest
    {


        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
       
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Currency { get; set; }
    }

}
