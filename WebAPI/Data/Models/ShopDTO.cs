using System.Data;
using System.Data.SqlClient;

using Domain.Models;

namespace Data.Models
{
    public class ShopDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public static ShopDTO MapFrom(SqlDataReader reader)
            => new ShopDTO
            {
                Id = reader.GetInt32(nameof(Id)),
                City = reader.GetString(nameof(City)),
                Address = reader.GetString(nameof(Address)),
                ZipCode = reader.GetInt32(nameof(ZipCode)),
                Phone = reader.GetString(nameof(Phone)),
                Email = reader.GetString(nameof(Email))
            };

        public Shop ToDomainModel()
            => new Shop
            {
                Id = this.Id,
                City = this.City,
                Address = this.Address,
                ZipCode = this.ZipCode,
                Phone = this.Phone,
                Email = this.Email
            };
    }
}