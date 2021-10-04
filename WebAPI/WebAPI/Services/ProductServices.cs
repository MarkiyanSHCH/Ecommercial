using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Services
{
    public class ProductServices
    {
        public readonly DataBase _database;
        public ProductServices()
        {
           _database = new DataBase();
        }

        public IEnumerable<Product> Get(IConfiguration _configuration) {
            //string query = "Exec ReadAllProducts";
            string query = "select * from Products; select * from Products";
            
            return _database.ReadDatabase(_configuration,query).AsEnumerable().Select(row => new Product{ 
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    Price = Convert.ToInt32(row["Price"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    PhotoFileName = Convert.ToString(row["photoFileName"])
            });
        }

        public Product GetById(IConfiguration _configuration, int id)
        {
            string query = @"Exec ReadProductsById @id = " + id;

            string queryChar = @"Exec ReadCharacteristicForProduct @idProduct = " + id;


            IEnumerable<Characteristics> characteristics = _database.ReadDatabase(_configuration, queryChar).AsEnumerable().Select(row => new Characteristics
            {
                Name = Convert.ToString(row["Name"]),
                Value = Convert.ToString(row["Value"])
            });


            return _database.ReadDatabase(_configuration, query).AsEnumerable().Select(row => new Product
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                Description = Convert.ToString(row["Description"]),
                Price = Convert.ToInt32(row["Price"]),
                CategoryId = Convert.ToInt32(row["CategoryId"]),
                PhotoFileName = Convert.ToString(row["PhotoFileName"]),
                characteristics = characteristics
            }).First();
            
        }

        public IEnumerable<Product> GetByCategory(IConfiguration _configuration, int id)
        {
            string query = @"Exec ReadProductsByCategory @id = " + id;

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
