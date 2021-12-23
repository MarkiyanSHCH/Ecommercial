using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Core.Domain.Products;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public ProductRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public IEnumerable<Product> GetAll()
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("ReadAllProducts", connection))
                {
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) productList.Add(ProductDTO.MapFrom(reader));
                }
                return productList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get products",
                   ApplicationScope.Products,
                   ex);

                return Enumerable.Empty<Product>();
            }
        }

        public Product GetById(int productId)
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command =
                    new SqlCommand("spProduct_GetProductByIdWithCharacteristic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@id", SqlDbType.Int)
                        .Value = productId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) productList.Add(ProductDTO.MapFrom(reader));
                }
                return ProductDTO.ToDomainModel(productList);
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get product by id",
                   ApplicationScope.Products,
                   new { productId },
                   ex);

                return null;
            }
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spProducts_GetByCategoryId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@id", SqlDbType.Int)
                        .Value = categoryId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) productList.Add(ProductDTO.MapFrom(reader));
                }
                return productList.Select(dto => dto.ToDomainModel());
            }

            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get products by category id",
                   ApplicationScope.Products,
                   new { categoryId },
                   ex);

                return Enumerable.Empty<Product>();
            }
        }

        public IEnumerable<Product> GetByIds(IEnumerable<int> productIds)
        {
            try
            {
                var itemsTable = new DataTable();
                var idColumn = new DataColumn("Id", typeof(int));
                itemsTable.TableName = "ItemId";
                itemsTable.Columns.AddRange(new DataColumn[] { idColumn });
                foreach (var id in productIds)
                    itemsTable.Rows.Add(id);

                var cartItemList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spProduct_GetByIds", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter udttParam = command.Parameters.AddWithValue("@ItemId", itemsTable);
                    udttParam.SqlDbType = SqlDbType.Structured;
                    udttParam.TypeName = "dbo.ItemId";

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) cartItemList.Add(ProductDTO.MapFrom(reader));
                }
                return cartItemList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get products by product's ids",
                   ApplicationScope.Products,
                   new { productIds },
                   ex);

                return Enumerable.Empty<Product>();
            }
        }
    }
}