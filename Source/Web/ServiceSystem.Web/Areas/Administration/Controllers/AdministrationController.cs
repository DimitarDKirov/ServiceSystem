namespace ServiceSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using ServiceSystem.Common;
    using ServiceSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
