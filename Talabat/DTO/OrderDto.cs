using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.DTO
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        
        public int  DeliveryMethodId{ get; set; }
        
        public AddressDto ShippinAddress{ get; set; }

    }
}
