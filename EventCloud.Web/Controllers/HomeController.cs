using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace TaskManager.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : TaskManagerControllerBase
    {
        public ActionResult Index()
        {
            //return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
            return View();
        }
	}
}