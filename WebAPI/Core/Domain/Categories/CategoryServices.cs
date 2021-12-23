using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Categories
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