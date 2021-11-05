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
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public static ProductDTO MapFrom(SqlDataReader reader)
             => new ProductDTO
             {
                 Id = reader.GetInt32(nameof(Id)),
                 Name = !reader.IsDBNull(reader.GetOrdinal(nameof(Name)))
                        ? reader.GetString(nameof(Name)) : null,
                 Description = !reader.IsDBNull(reader.GetOrdinal(nameof(Description)))
                        ? reader.GetString(nameof(Description)) : null,
                 Price = !reader.IsDBNull(reader.GetOrdinal(nameof(Price)))
                        ? reader.GetDouble(nameof(Price)) : 0,
                 CategoryId = !reader.IsDBNull(reader.GetOrdinal(nameof(CategoryId)))
                        ? reader.GetInt32(nameof(CategoryId)) : 0,
                 PhotoFileName = !reader.IsDBNull(reader.GetOrdinal(nameof(PhotoFileName)))
                        ? reader.GetString(nameof(PhotoFileName)) : null,
                 PropertyName = HasColumn(reader, nameof(PropertyName))
                        ? !reader.IsDBNull(reader.GetOrdinal(nameof(PropertyName)))
                        ? reader.GetString(nameof(PropertyName)) : null
                        : null,
                 PropertyValue = HasColumn(reader, nameof(PropertyValue))
                        ? !reader.IsDBNull(reader.GetOrdinal(nameof(PropertyValue)))
                        ? reader.GetString(nameof(PropertyValue)) : null
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
            product.Properties = productList.First().PropertyValue != null
                ? productList.Select(item =>
                    new Property
                    {
                        Name = item.PropertyName,
                        Value = item.PropertyValue
                    })
                : Enumerable.Empty<Property>();

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