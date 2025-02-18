using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.ISpecifications.Order_Spec
{
    public class OrderSpecifications:BaseSpecification<Order>
    {

        public OrderSpecifications(string email)
            : base(O => O.BuyerEmail == email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDes(O => O.OrderDate);

        }
        public OrderSpecifications(int id,string email)
           : base(O => O.BuyerEmail == email && O.Id == id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDes(O => O.OrderDate);

        }


    }
}
