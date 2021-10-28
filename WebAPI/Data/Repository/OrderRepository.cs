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

        public IEnumerable<Order> GetAllOrders(int UserId)
        {
            try
            {
                var orderList = new List<OrderDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spOrders_GetOrderByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@UserId", SqlDbType.Int)
                        .Value = UserId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) orderList.Add(OrderDTO.MapFrom(reader));
                }
                return orderList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<OrderLine> GetAllOrderLines(int OrderId)
        {
            try
            {
                var orderLineList = new List<OrderLineDTO>();

                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spOrderLines_GetOrderLinesByOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@OrderId", SqlDbType.Int)
                        .Value = OrderId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) orderLineList.Add(OrderLineDTO.MapFrom(reader));
                }
                return orderLineList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddOrderProduct(
            int UserId,
            int ShopId,
            double TotalPrice,
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

                using var connection = new SqlConnection(_sqlDataSource);
                using var command = new SqlCommand("spOrders_AddOrderItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                command.Parameters.Add("@ShopId", SqlDbType.Int).Value = ShopId;
                command.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = TotalPrice;
                SqlParameter udttParam = command.Parameters.AddWithValue("@OrderLines", linesTable);
                udttParam.SqlDbType = SqlDbType.Structured;
                udttParam.TypeName = "dbo.Lines";

                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /* public bool RemoveOrderProduct(int UserId, int ProductId)
         {
             try
             {
                 using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                 using (SqlCommand command = new SqlCommand("spOrders_RemoveOrderItem", connection))
                 {
                     command.CommandType = CommandType.StoredProcedure;
                     command.Parameters
                         .Add("@UserId", SqlDbType.Int)
                         .Value = UserId;
                     command.Parameters
                         .Add("@ProductId", SqlDbType.Int)
                         .Value = ProductId;

                     connection.Open();
                     command.ExecuteNonQuery();
                 }
                 return true;
             }
             catch (Exception ex)
             {
                 return false;
             }
         }*/
    }
}