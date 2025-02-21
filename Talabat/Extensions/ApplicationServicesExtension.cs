using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.IRepositories;
using Talabat.Core.IServies;
using Talabat.Errors;
using Talabat.Helper;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IPaymentService, PaymentService>();
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContex) => {

                    var errors = actionContex.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(P => P.Value.Errors).Select(E => E.ErrorMessage).ToArray();
                    var validtionErrorRespose = new ApiValidationErrorResponse()
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(validtionErrorRespose);
                };
            });
            return services;
        }
    }
}
