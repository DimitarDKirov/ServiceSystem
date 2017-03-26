using System.Web.Mvc;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceSystem.Infrastructure;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.ViewModels.Orders;

namespace ServiceSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
    public class OrdersController : BaseController
    {
        private IOrderService orderService;

        public OrdersController(IOrderService ordersService)
        {
            Guard.WhenArgument(ordersService, "ordersService").IsNull().Throw();
            this.orderService = ordersService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult OrdersRead([DataSourceRequest]DataSourceRequest request)
        {
            var orders = this.orderService
                .GetAsQuaryable()
                .To<OrdersGridViewModel>()
                .ToDataSourceResult(request);

            return this.Json(orders);
        }
    }
}
