using System;
using System.Data;
using System.Data.SqlClient;

using Domain.Models;

namespace Data.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int ShopId { get; set; }
        public DateTime OrderDate { get; set; }

        public static OrderDTO MapFrom(SqlDataReader reader)
             => new OrderDTO
             {
                 Id = reader.GetInt32(nameof(Id)),
                 TotalPrice = !reader.IsDBNull(reader.GetOrdinal("TotalPrice"))
                        ? reader.GetDouble(nameof(TotalPrice)) : 0,
                 ShopId = !reader.IsDBNull(reader.GetOrdinal("ShopId"))
                        ? reader.GetInt32(nameof(ShopId)) : 0,
                 OrderDate = reader.GetDateTime(nameof(OrderDate))
             };

        public Order ToDomainModel()
            => new Order
            {
                Id = this.Id,
                TotalPrice = this.TotalPrice,
                ShopId = this.ShopId,
                OrderDate = this.OrderDate
            };
    }
}
