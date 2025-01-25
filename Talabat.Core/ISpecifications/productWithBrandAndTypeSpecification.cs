using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.ISpecifications
{
  public class productWithBrandAndTypeSpecification:BaseSpecification<Product>
    {

        public productWithBrandAndTypeSpecification()
        {
            Includes.Add(a => a.ProductBrand);
            Includes.Add(a => a.ProductType);
        }

        public productWithBrandAndTypeSpecification( int id):base(P => P.Id == id)
        {
            Includes.Add(a => a.ProductBrand);
            Includes.Add(a => a.ProductType);
        }
    }
}
