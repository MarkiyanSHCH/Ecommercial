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

        public IEnumerable<Order> GetAllOrders(int UserId)
            => this._orderRepository.GetAllOrders(UserId);

        public IEnumerable<OrderLine> GetAllOrderLines(int OrderId)
           => this._orderRepository.GetAllOrderLines(OrderId);

        public int AddOrderProduct(int UserId, int ShopId, double TotalPrice, IEnumerable<OrderLine> orderLines)
            => this._orderRepository.AddOrderProduct(UserId, ShopId, TotalPrice, orderLines);

        /* public bool RemoveOrderProduct(int UserId, int ProductId)
             => this._orderRepository.RemoveOrderProduct(UserId, ProductId);*/
    }
}