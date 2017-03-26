using System.Collections.Generic;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAll();

        CategoryModel Find(int id);

        CategoryModel UpdateById(int id, string name, decimal minPrice, decimal maxPrice);

        void Delete(CategoryModel category);

        void Create(string name, decimal minPrice, decimal maxPrice);
    }
}
