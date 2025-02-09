using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO
{
    public class BasketItemDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "Price must be greater then zero !!")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be one Item at Least!!")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }

    }
}