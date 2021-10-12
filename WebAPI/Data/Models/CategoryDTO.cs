using System.Data;
using System.Data.SqlClient;

using Domain.Models;

namespace Data.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CategoryDTO MapFrom(SqlDataReader reader)
            => new CategoryDTO
            {
                Id = reader.GetInt32(nameof(Id)),
                Name = reader.GetString(nameof(Name))
            };

        public Category ToDomainModel()
            => new Category
            {
                Id = this.Id,
                Name = this.Name
            };
    }
}