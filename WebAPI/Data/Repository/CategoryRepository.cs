using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Core.Domain.Categories;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public CategoryRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public IEnumerable<Category> GetAll()
        {
            try
            {
                var categoryList = new List<CategoryDTO>();

                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("ReadAllCategories", connection))
                {
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) categoryList.Add(CategoryDTO.MapFrom(reader));
                }
                return categoryList.Select(dto => dto.ToDomainModel());
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get categories.",
                   ApplicationScope.Categories,
                   ex);

                return Enumerable.Empty<Category>();
            }
        }
    }
}