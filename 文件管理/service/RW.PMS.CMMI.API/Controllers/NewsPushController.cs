using Microsoft.AspNetCore.Mvc;

namespace RW.PMS.CMMI.API.Controllers
{
    public class NewsPushController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
