using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.DTO
{
    public class CustomerBasketDto
    {
        [Required]
        public int Id { get; set; }
        public List<BasketItemDto> Items { get; set; } 
    }
}
