using System;
using System.Linq;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private IEfDbRepository<Category> categoriesRepo;

        public CategoriesService(IEfDbRepository<Category> categories)
        {
            this.categoriesRepo = categories;
        }

        public Category Create(string name, decimal minPrice, decimal maxPrice)
        {
            var category = new Category
            {
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            this.categoriesRepo.Add(category);
            this.categoriesRepo.Save();
            return category;
        }

        public void Delete(Category category)
        {
            this.categoriesRepo.Delete(category);
            this.categoriesRepo.Save();
            return;
        }

        public Category Find(int id)
        {
            return this.categoriesRepo.GetById(id);
        }

        public IQueryable<Category> GetAll()
        {
            return this.categoriesRepo
                .All();
        }

        public Category UpdateById(int id, string name, decimal minPrice, decimal maxPrice)
        {
            var category = this.categoriesRepo.GetById(id);
            if (category == null)
            {
                throw new ArgumentException("Category can not be found");
            }

            category.Name = name;
            category.MinPrice = minPrice;
            category.MaxPrice = maxPrice;
            this.categoriesRepo.Save();
            return category;
        }
    }
}
