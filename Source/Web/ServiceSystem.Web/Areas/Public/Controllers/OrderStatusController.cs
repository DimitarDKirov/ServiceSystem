namespace ServiceSystem.Web.Areas.Public.Controllers
{
    using System;
    using System.Web.Mvc;
    using Models.OrderStatus;
    using Services.Data;
    using Services.Web;
    using Web.Controllers;
    using Services.Data.Contracts;
    using Infrastructure.PublicCodeProvider;

    public class OrderStatusController : BaseController
    {
        private IPublicCodeProvider coder;
        private IOrderService orderService;

        public OrderStatusController(IPublicCodeProvider coder, IOrderService orderService)
        {
            this.coder = coder;
            this.orderService = orderService;
        }

        // GET: Public/OrderStatus
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OrderSearchViewModel input)
        {
            int orderId;
            try
            {
                orderId = this.coder.Decode(input.UserInput);
            }
            catch (Exception)
            {
                input.Result = "Incorrect code";
                return this.View(input);
            }

            var order = this.orderService.GetById(orderId);
            if (order == null)
            {
                this.ModelState.AddModelError("UserInput", "Such order can not be found");
                input.Result = "Such order can not be found";
                return this.View(input);
            }

            if (order.OrderPublicId != input.UserInput)
            {
                input.Result = "Incorrect code";
                this.ModelState.AddModelError("UserInput", "Code is not correct");
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var model = this.Mapper.Map<OrderStatusViewModel>(order);
            return this.View("OrderStatus", model);
        }
    }
}
