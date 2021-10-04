using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Services
{
    public class CategoryServices
    {
        public readonly DataBase _database;
        public CategoryServices()
        {
            _database = new DataBase();
        }
        public IEnumerable<Category> Get(IConfiguration _configuration)
        {
            string query = "Exec ReadAllCategories";

            return _database.ReadDatabase(_configuration, query).AsEnumerable().Select(row => new Category
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
            });
        }

    }
}
