using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IRepositories;
using Talabat.Core.IServies;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basket,
           IUnitOfWork unitOfWork
            )
        {
            _configuration = configuration;
            _basket = basket;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var basket = await _basket.GetBasketAsync(basketId);
            if (basket is null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliverymethod = await _unitOfWork.Repository<DeliveryMethod>().GetbyIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingCost = deliverymethod.Cost;
                shippingPrice = deliverymethod.Cost;
            }

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetbyIdAsync(item.Id);

                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }


                }


            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))//Create Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                };

                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update PaymentIntend
            {
                var options = new PaymentIntentUpdateOptions() {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100,

                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basket.UpdateBasketAsync(basket);

            return basket;
            } 

        }
    }
