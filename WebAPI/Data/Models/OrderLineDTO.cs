using System.Data;
using System.Data.SqlClient;

using Domain.Models;

namespace Data.Models
{
    public class OrderLineDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public static OrderLineDTO MapFrom(SqlDataReader reader)
             => new OrderLineDTO
             {
                 Id = reader.GetInt32(nameof(Id)),
                 OrderId = reader.GetInt32(nameof(OrderId)),
                 Note = !reader.IsDBNull(reader.GetOrdinal("Note"))
                        ? reader.GetString(nameof(Note)) : null,
                 Quantity = reader.GetInt32(nameof(Quantity)),
                 ProductId = reader.GetInt32(nameof(ProductId)),
                 Name = !reader.IsDBNull(reader.GetOrdinal("Name"))
                        ? reader.GetString(nameof(Name)) : null,
                 Price = !reader.IsDBNull(reader.GetOrdinal("Price"))
                        ? reader.GetDouble(nameof(Price)) : 0

             };

        public OrderLine ToDomainModel()
            => new OrderLine
            {
                Id = this.Id,
                OrderId = this.OrderId,
                Note = this.Note,
                Quantity = this.Quantity,
                ProductId = this.ProductId,
                Name = this.Name,
                Price = this.Price
            };
    }
}
