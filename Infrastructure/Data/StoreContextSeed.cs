
using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public async Task SeedJsonAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrand.Any())
                {
                    var brandsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    await context.BulkInsertAsync(brands);
                }

                if (!context.ProductType.Any())
                {
                    var typesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    await context.BulkInsertAsync(types);
                }

                if (!context.Product.Any())
                {
                    var ProductData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    await context.BulkInsertAsync(products);
                }

                if (!context.DeliveryMethods.Any())
                {
                    var deliveryData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                    await context.BulkInsertAsync(deliveryMethods);
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}