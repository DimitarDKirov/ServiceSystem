using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceSystem.Data.Models;
using ServiceSystem.Data.Common;

namespace ServiceSystem.Services.Data
{
    class CategoriesService : ICategoriesService
    {
        private IDbRepository<Category> categoriesRepo;

        public CategoriesService(IDbRepository<Category> categories)
        {
            this.categoriesRepo = categories;
        }

        public IQueryable<Category> GetAllForCombo()
        {
            return this.categoriesRepo
                .All();
        }
    }
}
