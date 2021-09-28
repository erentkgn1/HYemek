using System.ComponentModel.DataAnnotations;

namespace HepsiYemek.API.Models
{
    public class UpdateProductRequest
    {
        [Required]
        public string ID { get; set; }
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
