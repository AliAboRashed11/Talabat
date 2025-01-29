using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Errors;

namespace Talabat.Controllers
{
    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }
        [HttpPost] 
        public async Task<ActionResult<CustomerBasket>> updateBasket(CustomerBasket Basket)
        {
            var basket = await _basketRepository.UpdateBasketAsync(Basket);

            if (basket is null) return BadRequest(new ApiResponse(400));
            return basket;
        }
        [HttpDelete("{id}")] 
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
