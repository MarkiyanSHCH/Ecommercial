using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Core.Domain.Orders;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public OrderRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public IEnumerable<Order> GetAllOrders(int userId)
        {
            try
            {
                var orderList = new List<OrderDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spOrders_GetOrderByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@UserId", SqlDbType.Int)
                        .Value = userId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) orderList.Add(OrderDTO.MapFrom(reader));
                }
                return orderList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                this._logger.Error(
                    "Cannot get orders by User Id",
                    ApplicationScope.Orders,
                    new { userId },
                    ex);

                return Enumerable.Empty<Order>();
            }
        }

        public IEnumerable<OrderLine> GetAllOrderLines(int orderId)
        {
            try
            {
                var orderLineList = new List<OrderLineDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spOrderLines_GetOrderLinesByOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@OrderId", SqlDbType.Int)
                        .Value = orderId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) orderLineList.Add(OrderLineDTO.MapFrom(reader));
                }
                return orderLineList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Cannot get order lines by Order Id",
                   ApplicationScope.Orders,
                   new { orderId },
                   ex);

                return Enumerable.Empty<OrderLine>();
            }
        }

        public int AddOrderProduct(
            int userId,
            int shopId,
            double totalPrice,
            IEnumerable<OrderLine> orderLines)
        {
            try
            {
                var linesTable = new DataTable { TableName = "Lines" };
                var noteColumn = new DataColumn("Note", typeof(string));
                var quantityColumn = new DataColumn("Quantity", typeof(int));
                var productIdColumn = new DataColumn("ProductId", typeof(int));
                linesTable.Columns.AddRange(new DataColumn[] {
                    noteColumn,
                    quantityColumn,
                    productIdColumn
                });
                foreach (OrderLine line in orderLines)
                    linesTable.Rows.Add(
                        line.Note,
                        line.Quantity,
                        line.ProductId);

                using var connection = new SqlConnection(this._settings.ConnectionString);
                using var command = new SqlCommand("spOrders_AddOrderItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                command.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;
                command.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = totalPrice;
                SqlParameter udttParam = command.Parameters.AddWithValue("@OrderLines", linesTable);
                udttParam.SqlDbType = SqlDbType.Structured;
                udttParam.TypeName = "dbo.Lines";

                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to add order and order line",
                   ApplicationScope.Orders,
                   new
                   {
                       userId,
                       shopId,
                       totalPrice,
                       orderLines
                   },
                   ex);

                return 0;
            }
        }
    }
}