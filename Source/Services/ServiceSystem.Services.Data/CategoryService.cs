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
    public class CategoryService : ICategoryService
    {
        private IEfDbRepository<Category> categoriesRepo;
        private IEfDbRepositorySaveChanges efRepoSaveChanges;
        private IMappingService mappingService;

        public CategoryService(IEfDbRepository<Category> categoriesRepo, IEfDbRepositorySaveChanges efRepoSaveChanges, IMappingService mappingService)
        {
            Guard.WhenArgument(categoriesRepo, "categoriesRepo").IsNull().Throw();
            Guard.WhenArgument(efRepoSaveChanges, "efRepoSaveChanges").IsNull().Throw();
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();

            this.categoriesRepo = categoriesRepo;
            this.efRepoSaveChanges = efRepoSaveChanges;
            this.mappingService = mappingService;
        }

        public void Create(string name, decimal minPrice, decimal maxPrice)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must be specified");
            }

            var category = new Category
            {
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            var savedCategory = this.categoriesRepo.Add(category);
            this.efRepoSaveChanges.SaveChanges();
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

        public CategoryModel Update(CategoryModel model)
        {
            var category = this.categoriesRepo.GetById(model.Id);
            if (category == null)
            {
                throw new ArgumentException("Category can not be found");
            }

            category.Name = model.Name;
            category.MinPrice = model.MinPrice;
            category.MaxPrice = model.MaxPrice;
            this.categoriesRepo.Update(category);
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
