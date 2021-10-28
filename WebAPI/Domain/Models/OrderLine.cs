namespace Domain.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}