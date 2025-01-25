using AutoMapper;
using AutoMapper.Execution;
using Talabat.Core.Entities;
using Talabat.DTO;
using static System.Net.WebRequestMethods;

namespace Talabat.Helper
{
    public class ProductUrlPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";

            return string.Empty ;
        }
    }
}
