using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;

namespace Talabat.Controllers
{
 
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepositry<Product> _repositry;

        public ProductController(IGenericRepositry<Product> repositry)
        {
            _repositry = repositry;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() {
        
            var Products = await _repositry.GetAllAsync();

            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult <IQueryable <Product>>> GetProduct(int id)
        {
            var product  = await _repositry.GetbyIdAsync(id);
            return Ok(product);
        }
    }
}
