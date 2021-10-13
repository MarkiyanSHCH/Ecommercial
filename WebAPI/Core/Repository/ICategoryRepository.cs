using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}