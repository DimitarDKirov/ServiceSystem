namespace ServiceSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Models.Categories;
    using Services.Data;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : BaseController
    {
        private ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categories)
        {
            this.categoriesService = categories;
        }

        public ActionResult Index()
        {
            var categories = this.categoriesService
                .GetAll()
                .To<CategoriesViewModel>()
                .ToList();

            return this.View(categories);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = "Wrong search data";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = this.categoriesService.Find((int)id);
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

            try
            {
                this.categoriesService.UpdateById(model.Id, model.Name, model.MinPrice, model.MaxPrice);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.View(model);
            }

            this.TempData["Success"] = "Category updated";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category category = this.categoriesService.Find(id);

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
            Category category = this.categoriesService.Find(id);
            this.categoriesService.Delete(category);
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
    }
}
