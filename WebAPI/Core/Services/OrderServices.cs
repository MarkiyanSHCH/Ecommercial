using System.Collections.Generic;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServices(IOrderRepository orderRepository)
            => this._orderRepository = orderRepository;

        public IEnumerable<Order> GetAllOrders(int userId)
            => this._orderRepository.GetAllOrders(userId);

        public IEnumerable<OrderLine> GetAllOrderLines(int orderId)
           => this._orderRepository.GetAllOrderLines(orderId);

        public int AddOrderProduct(int userId, int shopId, double totalPrice, IEnumerable<OrderLine> orderLines)
            => this._orderRepository.AddOrderProduct(userId, shopId, totalPrice, orderLines);
    }
}