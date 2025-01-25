using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.ISpecifications;
using Talabat.DTO;
using Talabat.Errors;

namespace Talabat.Controllers
{
 
    public class ProductController : BaseApiController
    {

       
        private readonly IGenericRepositry<Product> _repositry;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepositry<Product> repositry,IMapper mapper)
        {
            _repositry = repositry;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts() {
         var spec = new productWithBrandAndTypeSpecification();

            var Products = await _repositry.GetAllWithSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(Products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult <IQueryable <ProductToReturnDto>>> GetProduct(int id)
        {
            var spec = new productWithBrandAndTypeSpecification(id);
            var product  = await _repositry.GetByIdWithSpecAsync(spec);
            if(product is null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto> (product));
               
        }
    }
}
