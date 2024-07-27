using Microsoft.AspNetCore.Mvc;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
