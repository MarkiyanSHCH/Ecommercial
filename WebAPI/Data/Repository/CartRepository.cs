using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Microsoft.Extensions.Configuration;

using Data.Models;
using Domain.Models;
using Core.Repository;

namespace Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly string _sqlDataSource;

        public CartRepository(IConfiguration configuration)
            => (this._sqlDataSource) = (configuration.GetConnectionString("ProductAppCon"));

        public IEnumerable<Product> GetCartItems(IEnumerable<int> productIds)
        {
            try
            {
                var itemsTable = new DataTable();
                var idColumn = new DataColumn("Id", typeof(int));
                itemsTable.TableName = "ProductCartSearch";
                itemsTable.Columns.AddRange(new DataColumn[] { idColumn });
                foreach (var id in productIds)
                    itemsTable.Rows.Add(id);

                var cartItemList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spProduct_GetCartItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter udttParam = command.Parameters.AddWithValue("@ItemId", itemsTable);
                    udttParam.SqlDbType = SqlDbType.Structured;
                    udttParam.TypeName = "dbo.ProductCartSearch";

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) cartItemList.Add(ProductDTO.MapFrom(reader));
                }
                return cartItemList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}