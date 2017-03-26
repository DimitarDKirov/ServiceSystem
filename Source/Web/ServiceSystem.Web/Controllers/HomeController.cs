using System.Web.Mvc;

namespace ServiceSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            return this.View();
        }
    }
}
