using System.ComponentModel.DataAnnotations;

namespace HepsiYemek.API.Models
{
    public class AddCategoryRequest
    {


        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
