using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.IServies
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail , string basketId,int delverMethodId,Entities.Order_Aggregate.Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdForUserAsync(int orderId,string buyerEmail);
      Task<IEnumerable<DeliveryMethod>>GetGetDeliveryMethodsAsync();
    }
}
