using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

using Microsoft.Extensions.Configuration;

using Data.Models;
using Domain.Models;
using Core.Repository;

namespace Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _sqlDataSource;

        public ProductRepository(IConfiguration configuration)
            => this._sqlDataSource = configuration.GetConnectionString("ProductAppCon");

        public IEnumerable<Product> GetAll()
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
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
                return Enumerable.Empty<Product>();
            }
        }

        public Product GetById(int id)
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command =
                    new SqlCommand("spProduct_GetProductByIdWithCharacteristic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@id", SqlDbType.Int)
                        .Value = id;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) productList.Add(ProductDTO.MapFrom(reader));
                }
                return ProductDTO.ToDomainModel(productList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Product> GetByCategory(int Id)
        {
            try
            {
                var productList = new List<ProductDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("ReadProductsByCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@id", SqlDbType.Int)
                        .Value = Id;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) productList.Add(ProductDTO.MapFrom(reader));
                }
                return productList.Select(dto => dto.ToDomainModel());
            }

            catch (Exception ex)
            {
                return Enumerable.Empty<Product>();
            }
        }
    }
}