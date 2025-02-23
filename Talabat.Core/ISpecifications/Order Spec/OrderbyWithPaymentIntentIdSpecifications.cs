using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.ISpecifications.Order_Spec
{
    public class OrderbyWithPaymentIntentIdSpecifications:BaseSpecification<Order>
    {
        public OrderbyWithPaymentIntentIdSpecifications(string PaymentIntentId)
            :base(O => O.PaymentIntentId  == PaymentIntentId)
        {
            
        }
    }
}
