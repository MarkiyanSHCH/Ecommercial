using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Core.Domain.Shops;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public ShopRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public IEnumerable<Shop> GetAll()
        {
            try
            {
                var shopList = new List<ShopDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
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
                this._logger.Error(
                   "Failed to get shops",
                   ApplicationScope.Shop,
                   ex);

                return Enumerable.Empty<Shop>();
            }
        }

        public Shop GetById(int shopId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spShops_GetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@ShopId", SqlDbType.Int, 100)
                        .Value = shopId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return ShopDTO.MapFrom(reader).ToDomainModel();
                }
                return null;
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get shop by shop id",
                   ApplicationScope.Auth,
                   new { shopId },
                   ex);

                return null;
            }
        }
    }
}