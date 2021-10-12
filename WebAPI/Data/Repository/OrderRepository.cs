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
    public class OrderRepository : IOrderRepository
    {
        private readonly string _sqlDataSource;

        public OrderRepository(IConfiguration configuration)
            => (this._sqlDataSource) = (configuration.GetConnectionString("ProductAppCon"));

        public IEnumerable<Product> GetAll(string Id)
        {
            var orderList = new List<ProductDTO>();

            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            using (SqlCommand command = new SqlCommand("spProduct_GetOrdersProduct", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Id);

                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) orderList.Add(ProductDTO.MapFrom(reader));
            }
            return orderList.Select(dto => dto.ToDomainModel());
        }
    }
}