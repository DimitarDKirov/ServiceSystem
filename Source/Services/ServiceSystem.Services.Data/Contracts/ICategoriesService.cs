using System.Collections.Generic;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryModel> GetAll();

        CategoryModel Find(int id);

        CategoryModel UpdateById(int id, string name, decimal minPrice, decimal maxPrice);

        void Delete(CategoryModel category);

        CategoryModel Create(string name, decimal minPrice, decimal maxPrice);
    }
}
