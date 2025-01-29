using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepositories;
using Talabat.Errors;
using Talabat.Helper;
using Talabat.Repository;

namespace Talabat.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepository<>));;
            services.AddAutoMapper(typeof(MappingProfiles));
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
