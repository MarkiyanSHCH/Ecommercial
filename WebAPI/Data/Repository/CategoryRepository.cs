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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _sqlDataSource;

        public CategoryRepository(IConfiguration configuration)
            => (this._sqlDataSource) = (configuration.GetConnectionString("ProductAppCon"));

        public IEnumerable<Category> GetAll()
        {
            var categoryList = new List<CategoryDTO>();

            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            using (SqlCommand command = new SqlCommand("ReadAllCategories", connection))
            {
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) categoryList.Add(CategoryDTO.MapFrom(reader));
            }
            return categoryList.Select(dto => dto.ToDomainModel());
        }
    }
}