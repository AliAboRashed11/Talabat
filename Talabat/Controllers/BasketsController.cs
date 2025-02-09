using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.DTO;
using Talabat.Errors;

namespace Talabat.Controllers
{
    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }
        [HttpPost] 
        public async Task<ActionResult<CustomerBasket>> updateBasket(CustomerBasketDto Basket)
        {
            var mapbasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(Basket);
            var basket = await _basketRepository.UpdateBasketAsync(mapbasket);

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
