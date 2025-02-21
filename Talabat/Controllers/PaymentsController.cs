using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IServies;
using Talabat.DTO;
using Talabat.Errors;

namespace Talabat.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
          
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400,"Problem With Your Basket"));


            return Ok(basket);
        }
    }
}
