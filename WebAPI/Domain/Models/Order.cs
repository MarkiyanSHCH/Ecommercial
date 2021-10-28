using System;

namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public int ShopId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
