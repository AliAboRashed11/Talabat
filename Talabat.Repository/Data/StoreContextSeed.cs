using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
   public class StoreContextSeed
    {
        public  static async Task SeedAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/ProductBreand.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)

                        await storeContext.Set<ProductBrand>().AddAsync(brand);
                    await storeContext.SaveChangesAsync();
                }

            }
            if (!storeContext.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Type.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await storeContext.Set<ProductType>().AddAsync(type);
                        await storeContext.SaveChangesAsync();
                    }
                }
            }

            if (!storeContext.Products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Product.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await storeContext.Set<Product>().AddAsync(product);
                        await storeContext.SaveChangesAsync();
                    }
                }
            }





        }
    }
    }

