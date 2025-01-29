using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.ISpecifications;

using Talabat.DTO;
using Talabat.Errors;
using Talabat.Helper;

namespace Talabat.Controllers
{

    public class ProductController : BaseApiController
    {


        private readonly IGenericRepositry<Product> _repositry;
        private readonly IMapper _mapper;
       
        private readonly IGenericRepositry<ProductBrand> _brandsRepo;
        private readonly IGenericRepositry<ProductType> _typesRepo;

        public ProductController(IGenericRepositry<Product> repositry, IMapper mapper,
           
            IGenericRepositry<ProductBrand> brandsRepo,
            IGenericRepositry<ProductType> typesRepo
            )
        {
            _repositry = repositry;
            _mapper = mapper;
            
           _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParms specParams) {
            var spec = new productWithBrandAndTypeSpecification(specParams);
            var products = await _repositry.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec =  new  ProductWithFilterationForCountSpecifisation(specParams);
            var count = await _repositry.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSiza, count, data));
        }
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IQueryable<ProductToReturnDto>>> GetProduct(int id)
        {
            var spec = new productWithBrandAndTypeSpecification(id);
            var product = await _repositry.GetByIdWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));

        }
        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrends() { 
        var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
        }
            
    }
}
