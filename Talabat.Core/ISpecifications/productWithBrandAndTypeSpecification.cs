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

        public productWithBrandAndTypeSpecification(ProductSpecParms specParams) : 
            base(p =>
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId .HasValue || p.ProductBrandId == specParams.BrandId.Value) && 
            (!specParams.TypeId .HasValue || p.ProductTypeId == specParams.TypeId.Value))
        {
            Includes.Add(a => a.ProductBrand);
            Includes.Add(a => a.ProductType);
            AddOrderBy(P => P.Name);
    
            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
                {
                    case "priceAsc":
                        OrderBy = p => p.Price;
                        break;
                    case "priceDesc":
                        OrderByDescending = p => p.Price;

                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;
                }
            }
            ApplyPagination(specParams.PageSiza * (specParams.PageIndex - 1), specParams.PageSiza);
        }

        public productWithBrandAndTypeSpecification( int id):base(P => P.Id == id)
        {
            Includes.Add(a => a.ProductBrand);
            Includes.Add(a => a.ProductType);

            
        }
    }
}
