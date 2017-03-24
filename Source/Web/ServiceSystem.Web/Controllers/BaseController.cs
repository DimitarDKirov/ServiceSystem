using System.Web.Mvc;
using AutoMapper;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Services.Web;

namespace ServiceSystem.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
