using System.ComponentModel.DataAnnotations;

namespace HepsiYemek.API.Models
{
    public class UpdateCategoryRequest
    {

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
