using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders(int userId);
        IEnumerable<OrderLine> GetAllOrderLines(int orderId);
        int AddOrderProduct(
            int userId,
            int shopId,
            double totalPrice,
            IEnumerable<OrderLine> orderLines);
    }
}