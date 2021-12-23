using Domain.Models;

namespace WebAPI.Models.Api.Orders
{
    public class PostOrderLineRequest
    {
        public string Note { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public OrderLine ToDomainModel()
            => new OrderLine
            {
                Note = this.Note,
                Quantity = this.Quantity,
                ProductId = this.ProductId
            };
    }
}