using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Domain.Models;

namespace Data.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string PhotoFileName { get; set; }
        public string CharacteristicsName { get; set; }
        public string CharacteristicsValue { get; set; }

        public static ProductDTO MapFrom(SqlDataReader reader)
             => new ProductDTO
             {
                 Id = reader.GetInt32(nameof(Id)),
                 Name = !reader.IsDBNull(reader.GetOrdinal("Name")) 
                        ? reader.GetString(nameof(Name)) : null,
                 Description = !reader.IsDBNull(reader.GetOrdinal("Description")) 
                        ? reader.GetString(nameof(Description)) : null,
                 Price = !reader.IsDBNull(reader.GetOrdinal("Price")) 
                        ? reader.GetDouble(nameof(Price)) : 0,
                 CategoryId = !reader.IsDBNull(reader.GetOrdinal("CategoryId")) 
                        ? reader.GetInt32(nameof(CategoryId)) : 0,
                 PhotoFileName = !reader.IsDBNull(reader.GetOrdinal("PhotoFileName")) 
                        ? reader.GetString(nameof(PhotoFileName)) : null,
                 CharacteristicsName = HasColumn(reader, "CharName") 
                        ? !reader.IsDBNull(reader.GetOrdinal("CharName"))
                        ? reader.GetString("CharName") : null 
                        : null,
                 CharacteristicsValue = HasColumn(reader, "CharValue")
                        ? !reader.IsDBNull(reader.GetOrdinal("CharValue"))
                        ? reader.GetString("CharValue") : null
                        : null,
             };

        public Product ToDomainModel()
            => new Product
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                CategoryId = this.CategoryId,
                PhotoFileName = this.PhotoFileName
            };

        public static Product ToDomainModel(List<ProductDTO> productList)
        {
            Product product = productList.First().ToDomainModel();
            product.Characteristics = productList.Select(item => 
                new Characteristics { Name = item.CharacteristicsName, Value = item.CharacteristicsValue });
            return product;
        }

        public static bool HasColumn(SqlDataReader Reader, string ColumnName)
        {
            foreach (DataRow row in Reader.GetSchemaTable().Rows)
                if (row["ColumnName"].ToString() == ColumnName) return true;

            return false;
        }
    }
}