using Microsoft.AspNetCore.Http.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.IServies;
using Talabat.Core.ISpecifications.Order_Spec;

namespace Talabat.Service
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderServices(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService

            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int delverMethodId,
            Core.Entities.Order_Aggregate.Address shippingAddress)
        {
            //1. Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //2.Get Selected Items at Basket From Product Repo
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0) {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetbyIdAsync(item.Id);
                    var productItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            //3.Calculate SubTotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            //4.Get Delivery Method From DeliveryMethods Repo
            var delverMethod = await _unitOfWork.Repository<DeliveryMethod>().GetbyIdAsync(delverMethodId);
            //5.Create Order
            var spec = new OrderbyWithPaymentIntentIdSpecifications(basket.PaymentIntentId);
            var exsitingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (exsitingOrder is not null) { 
            _unitOfWork.Repository<Order>().Delete(exsitingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }
            var order = new Order(buyerEmail, shippingAddress, delverMethod, orderItems, subtotal, basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().Add(order);
            //6.Save To Database [TODO]
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            return order;
        }

     

    

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecifications(orderId, buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetGetDeliveryMethodsAsync()
        {
           var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;



        }
    } 
}
