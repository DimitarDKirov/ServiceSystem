namespace ServiceSystem.Web.Controllers
{
    using System.Web.Mvc;

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
