using System.Web.Mvc;
using ServiceSystem.Infrastructure;
using ServiceSystem.Web.Controllers;

namespace ServiceSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
