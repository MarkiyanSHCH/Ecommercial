using System.Collections.Generic;
using System.Linq;

using Core.Domain.Products;
using Core.Domain.Shops;
using Core.Handlers.Emails;
using Core.Domain.Orders.Models;

using Domain.Models;

namespace Core.Domain.Orders
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        public OrderServices(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IShopRepository shopRepository,
            IEmailNotificationService emailNotificationService)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
            this._shopRepository = shopRepository;
            this._emailNotificationService = emailNotificationService;
        }

        public IEnumerable<Order> GetAllOrders(int userId)
            => this._orderRepository.GetAllOrders(userId);

        public IEnumerable<OrderLine> GetAllOrderLines(int orderId)
           => this._orderRepository.GetAllOrderLines(orderId);

        public int AddOrderProduct(
            int userId,
            string userEmail,
            int shopId,
            double totalPrice,
            IEnumerable<OrderLine> orderLines)
        {
            int orderNumber = this._orderRepository.AddOrderProduct(userId, shopId, totalPrice, orderLines);

            IEnumerable<Product> productList = this._productRepository.GetByIds(orderLines.Select(line => line.ProductId));
            orderLines = orderLines.Select(line =>
            {
                Product product = productList.FirstOrDefault(product => product.Id == line.ProductId);
                line.Name = product.Name;
                line.Price = product.Price;
                line.PhotoFileName = product.PhotoFileName;
                return line;
            });

            if (!this._emailNotificationService.NotifyAsync<OrderConfirmationNotification>(
                subject: $"Order {orderNumber} - Confirmation",
                toAddress: userEmail,
                fromAddress: "test@ecommercial.com",
                emailTemplateName: "OrderEmail",
                model: new OrderConfirmationNotification
                {
                    OrderNumber = orderNumber,
                    TotalPrice = totalPrice,
                    ShippingAddress = this._shopRepository.GetById(shopId).Address,
                    OrderList = orderLines
                }).Result)

                return 0;

            return orderNumber;
        }
    }
}