using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, string dataPath, ILoggerFactory loggerFactory)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dataPath = path + "/Data/SeedData/";

            try
            {
                if (!context.AppInfos.Any())
                {
                    var appsData =
                        File.ReadAllText(dataPath + "apps.json");
                    var apps = JsonSerializer.Deserialize<List<AppInfo>>(appsData);
                    context.AppInfos.AddRange(apps);
                    await context.SaveChangesAsync();
                }

                if (!context.Customers.Any())
                {
                    var customersData =
                        File.ReadAllText(dataPath + "customers.json");
                    var customers = JsonSerializer.Deserialize<List<Customer>>(customersData);
                    context.Customers.AddRange(customers);
                    await context.SaveChangesAsync();
                }
                
                if (!context.ProductBrands.Any())
                {
                    var brandsData =
                        File.ReadAllText(dataPath + "brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    context.ProductBrands.AddRange(brands);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText(dataPath + "types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    context.ProductTypes.AddRange(types);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText(dataPath + "products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }

                 if (!context.DeliveryMethods.Any())
                {
                    var dmData =
                        File.ReadAllText(dataPath + "delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                    context.DeliveryMethods.AddRange(methods);
                    await context.SaveChangesAsync();
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