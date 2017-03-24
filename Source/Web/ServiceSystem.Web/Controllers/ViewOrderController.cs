namespace ServiceSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels;
    using ViewModels.ViewOrder;
    using ServiceSystem.Infrastructure;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
    public class ViewOrderController : BaseController
    {
        private IOrderService orderService;

        public ViewOrderController(IOrderService orders)
        {
            this.orderService = orders;
        }

        public ActionResult AfterCreation(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            return this.View(orderViewModel);
        }

        public ActionResult Details(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View();
            }

            var orderViewModel = this.Mapper.Map<OrderDetailsViewModel>(order);
            orderViewModel.IsDeliverable = false;
            if (order.Status == Status.Pending)
            {
                orderViewModel.IsAssignable = true;
                orderViewModel.IsEditable = false;
            }
            else if (order.Status == Status.Delivered)
            {
                orderViewModel.IsAssignable = false;
                orderViewModel.IsEditable = false;
            }
            else if (order.UserId == this.User.Identity.GetUserId())
            {
                orderViewModel.IsAssignable = false;
                orderViewModel.IsEditable = true;
            }
            else
            {
                orderViewModel.IsAssignable = false;
                orderViewModel.IsEditable = false;
            }

            if (order.Status == Status.Ready)
            {
                orderViewModel.IsDeliverable = true;
            }

            return this.View(orderViewModel);
        }

        [ChildActionOnly]
        public ActionResult CommonDetails(int id)
        {
            var order = this.orderService.GetById(id);
            var orderViewModel = this.Mapper.Map<CommonDetailsViewModel>(order);

            return this.PartialView("_OrderViewPartial", orderViewModel);
        }
    }
}
