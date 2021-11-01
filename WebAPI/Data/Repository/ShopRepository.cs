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
    public class ShopRepository : IShopRepository
    {
        private readonly string _sqlDataSource;

        public ShopRepository(IConfiguration configuration)
            => this._sqlDataSource = configuration.GetConnectionString("ProductAppCon");

        public IEnumerable<Shop> GetAll()
        {
            try
            {
                var shopList = new List<ShopDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spShops_GetAll", connection))
                {
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) shopList.Add(ShopDTO.MapFrom(reader));
                }
                return shopList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Shop>();
            }
        }

        public Shop GetById(int ShopId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spShops_GetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@ShopId", SqlDbType.Int, 100)
                        .Value = ShopId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return ShopDTO.MapFrom(reader).ToDomainModel();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
