using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.Extensions.Configuration;

using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Services
{
    public class OrderServices
    {
        public readonly DataBase _database;
        public OrderServices()
        {
            _database = new DataBase();
        }

        public IEnumerable<Product> GetOrders(IConfiguration _configuration, string UserId)
        {
            string query = "Exec spProduct_GetOrdersProduct @UserId = " + UserId;

            return _database.ReadDatabase(_configuration, query).AsEnumerable().Select(row => new Product
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                Price = Convert.ToInt32(row["Price"]),
                CategoryId = Convert.ToInt32(row["CategoryId"]),
                PhotoFileName = Convert.ToString(row["photoFileName"])
            });
        }

    }
}
