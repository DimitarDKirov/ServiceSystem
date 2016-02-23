namespace ServiceSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data;
    using Services.Web;
    using ViewModels.CreateOrder;

    public class CreateOrderController : BaseController
    {
        private ICategoriesService categoriesService;
        private IOrderService orderService;
        private ICustomerService customerService;
        private IUnitService unitService;
        private IPublicCodeProvider coder;

        public CreateOrderController(ICategoriesService categories, IOrderService orders, ICustomerService customers, IUnitService units, IPublicCodeProvider coder)
        {
            this.categoriesService = categories;
            this.orderService = orders;
            this.customerService = customers;
            this.unitService = units;
            this.coder = coder;
        }

        public ActionResult Add()
        {
            var model = new OrderCreateModel
            {
                Categories = this.GetCategories()
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(OrderCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.GetCategories();
                return this.View(model);
            }

            Order orderCreated = null;
            try
            {
                var customer = this.customerService.Create(model.Customer.Name, model.Customer.Phone, model.Customer.Email);
                var unit = this.unitService.Create(model.Unit.Brand, model.Unit.Model, model.Unit.SerialNumber, model.Unit.CategoryId);

                var order = new Order
                {
                    Customer = customer,
                    ProblemDescription = model.ProblemDescription,
                    Status = Status.Pending,
                    Unit = unit,
                    WarrantyStatus = model.WarrantyStatus,
                    WarrantyCard = model.WarrantyCard,
                    WarrantyDate = model.WarrantyDate,
                };

                this.orderService.Create(order);
                string publicCode = this.coder.Encode(order.Id, customer.Name);
                this.orderService.AddPublicId(order.Id, publicCode);
                orderCreated = order;
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = "Order can not be created";
                this.Response.StatusCode = 400;
                model.Categories = this.GetCategories();
                return this.View(model);
            }

            this.TempData["Success"] = "Order created successfully";
            return this.RedirectToAction("AfterCreation", "ViewOrder", new { id = orderCreated.Id });
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories =
                this.Cache.Get(
                    "categoriesCombo",
                    () => this.categoriesService
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList(),
                    24 * 60 * 60);
            return categories;
        }
    }
}