using BusinessLogic;
using BusinessLogic.Interface;
using DataAccess.Contexts;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reflection;

namespace Factory
{
    public class ServiceFactory
    {
        private readonly IServiceCollection services;

        public ServiceFactory(IServiceCollection services)
        {
            this.services = services;
        }

        public void AddCustomServices()
        {
            services.AddScoped<IBrandDiscountRepository, BrandDiscountRepository>();
            services.AddScoped<IBrandDiscountLogic, BrandDiscountLogic>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IAdminTokenRepository, AdminTokenRepository>();
            services.AddScoped<IAdminTokenLogic, AdminTokenLogic>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandLogic, BrandLogic>();
            services.AddScoped<IColorDiscountRepository, ColorDiscountRepository>();
            services.AddScoped<IColorDiscountLogic,ColorDiscountLogic >();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IColorLogic, ColorLogic>();
            services.AddScoped<IPercentageDiscountRepository, PercentageDiscountRepository>();
            services.AddScoped<IPercentageDiscountLogic, PercentageDiscountLogic>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductLogic, ProductLogic>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseLogic, PurchaseLogic>();
            services.AddScoped<IQuantityDiscountRepository, QuantityDiscountRepository>();
            services.AddScoped<IQuantityDiscountLogic, QuantityDiscountLogic>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleLogic, RoleLogic>();
            services.AddScoped<IDiscountLogic, DiscountLogic>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPaymentMethodLogic, PaymentMethodLogic>();
            services.AddScoped<IReflectionImplementation, ReflectionImplementation>();
        }

        public void AddDbContextService()
        {
            services.AddDbContext<DbContext, DataBaseContext>();
        }
    }
}
