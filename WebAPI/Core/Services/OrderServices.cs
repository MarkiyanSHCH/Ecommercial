using System.Collections.Generic;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServices(IOrderRepository orderRepository)
            => (this._orderRepository) = (orderRepository);

        public IEnumerable<Product> GetOrders(string UserId)
            => this._orderRepository.GetAll(UserId);
    }
}