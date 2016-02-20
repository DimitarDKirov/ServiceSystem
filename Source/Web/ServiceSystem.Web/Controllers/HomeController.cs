namespace ServiceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;

    using Services.Data;


    public class HomeController : BaseController
    {


        public ActionResult Index()
        {


            return this.View();
        }
    }
}
