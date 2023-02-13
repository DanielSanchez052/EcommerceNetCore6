using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketsAsync(basketId);

            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();
            
            if (result <= 0) return null;

            await _basketRepository.DeleteBasketsAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(string buyerEmail)
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
