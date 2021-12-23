using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Categories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}