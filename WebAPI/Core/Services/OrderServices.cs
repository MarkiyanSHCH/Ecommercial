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

        public IEnumerable<Product> GetOrders(int UserId)
            => this._orderRepository.GetAll(UserId);

        public bool AddOrderProduct(int UserId, int ProductId)
            => this._orderRepository.AddOrderProduct(UserId, ProductId);

        public bool RemoveOrderProduct(int UserId, int ProductId)
            => this._orderRepository.RemoveOrderProduct(UserId, ProductId);
    }
}