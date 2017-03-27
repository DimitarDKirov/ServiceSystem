using System;
using System.Web.Mvc;
using ServiceSystem.Infrastructure;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Services.Web;
using ServiceSystem.Web.Areas.Administration.Models.Categories;
using ServiceSystem.Web.Controllers;
using System.Collections.Generic;
using Bytes2you.Validation;

namespace ServiceSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : BaseController
    {
        private ICategoryService categoriesService;
        private ICacheService cacheService;

        public CategoriesController(ICategoryService categoriesService)
        {
            Guard.WhenArgument(categoriesService, "categoriesService").IsNull().Throw();
            this.categoriesService = categoriesService;
        }

        public ActionResult Index()
        {
            var categories = this.categoriesService.GetAll();
            var models = this.Mapper.Map<ICollection<CategoriesViewModel>>(categories);

            return this.View(models);
        }

        public ActionResult Edit(int id)
        {
            CategoryModel category = this.categoriesService.Find((int)id);
            if (category == null)
            {
                this.TempData["Error"] = "Category could not be found";
                return this.HttpNotFound();
            }

            var categoryModel = this.Mapper.Map<CategoryEditModel>(category);

            return this.View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditModel model)
        {
            this.CheckPrices(model.MinPrice, model.MaxPrice);

            if (!this.ModelState.IsValid)
            {
                this.TempData["Error"] = "Wrong data";
                return this.View(model);
            }

            var categoryModel = this.Mapper.Map<CategoryModel>(model);
            try
            {
                this.categoriesService.Update(categoryModel);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.View(model);
            }

            this.InvalidateCache();
            this.TempData["Success"] = "Category updated";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CategoryModel category = this.categoriesService.Find(id);

            if (category == null)
            {
                this.TempData["Error"] = "Category could not be found";
                return this.HttpNotFound();
            }

            var categoryModel = this.Mapper.Map<CategoryEditModel>(category);
            return this.View(categoryModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryModel category = this.categoriesService.Find(id);
            this.categoriesService.Delete(category);
            this.InvalidateCache();
            this.TempData["Success"] = "Category deleted";
            return this.RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryEditModel model)
        {
            this.CheckPrices(model.MinPrice, model.MaxPrice);

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.categoriesService.Create(model.Name, model.MinPrice, model.MaxPrice);
            this.InvalidateCache();
            this.TempData["Success"] = "Category added";
            return this.RedirectToAction("Index");
        }

        private void CheckPrices(decimal minPrice, decimal maxPrice)
        {
            if (minPrice > maxPrice)
            {
                this.TempData["Error"] = "Wrong data";
                this.ModelState.AddModelError(string.Empty, "Min price must be less than or equal to Max price");
            }

            return;
        }

        private void InvalidateCache()
        {
            this.Cache.Remove("categories");
        }
    }
}
