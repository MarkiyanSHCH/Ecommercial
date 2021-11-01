using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders(int UserId);
        IEnumerable<OrderLine> GetAllOrderLines(int OrderId);
        int AddOrderProduct(
            int UserId,
            int ShopId,
            double TotalPrice,
            IEnumerable<OrderLine> orderLines);
    }
}