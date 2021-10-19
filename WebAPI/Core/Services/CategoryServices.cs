using System.Collections.Generic;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class CategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
            => this._categoryRepository = categoryRepository;

        public IEnumerable<Category> Get()
            => this._categoryRepository.GetAll();
    }
}