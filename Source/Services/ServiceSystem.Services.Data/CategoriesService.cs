using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private IEfDbRepository<Category> categoriesRepo;
        private IEfDbRepositorySaveChanges efRepoSaveChanges;
        private IMappingService mappingService;

        public CategoriesService(IEfDbRepository<Category> categoriesRepo, IEfDbRepositorySaveChanges efRepoSaveChanges, IMappingService mappingService)
        {
            Guard.WhenArgument(categoriesRepo, "categoriesRepo").IsNull().Throw();
            Guard.WhenArgument(efRepoSaveChanges, "efRepoSaveChanges").IsNull().Throw();
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();

            this.categoriesRepo = categoriesRepo;
            this.efRepoSaveChanges = efRepoSaveChanges;
            this.mappingService = mappingService;
        }

        public CategoryModel Create(string name, decimal minPrice, decimal maxPrice)
        {
            var category = new Category
            {
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            this.categoriesRepo.Add(category);
            this.efRepoSaveChanges.SaveChanges();
            return this.mappingService.Map<CategoryModel>(category);
        }

        public CategoryModel Find(int id)
        {
            var category = this.categoriesRepo.GetById(id);
            return this.mappingService.Map<CategoryModel>(category);
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            return this.categoriesRepo
                .All()
                .To<CategoryModel>()
                .ToList();
        }

        public CategoryModel UpdateById(int id, string name, decimal minPrice, decimal maxPrice)
        {
            var category = this.categoriesRepo.GetById(id);
            if (category == null)
            {
                throw new ArgumentException("Category can not be found");
            }

            category.Name = name;
            category.MinPrice = minPrice;
            category.MaxPrice = maxPrice;
            this.efRepoSaveChanges.SaveChanges();
            return this.mappingService.Map<CategoryModel>(category);
        }

        public void Delete(CategoryModel categoryModel)
        {
            var category = this.mappingService.Map<Category>(categoryModel);
            this.categoriesRepo.Delete(category);
            this.efRepoSaveChanges.SaveChanges();
        }
    }
}
