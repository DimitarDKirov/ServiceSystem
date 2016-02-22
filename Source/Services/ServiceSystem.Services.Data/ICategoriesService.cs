namespace ServiceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceSystem.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Category Find(int id);

        Category UpdateById(int id, string name, decimal minPrice, decimal maxPrice);

        void Delete(Category category);

        Category Create(string name, decimal minPrice, decimal maxPrice);
    }
}
