using ShopManagement.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Domain.ProductCategoryAgg;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstraper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
