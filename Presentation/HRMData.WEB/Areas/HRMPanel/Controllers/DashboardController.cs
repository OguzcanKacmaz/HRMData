using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.Controllers
{
    [Authorize]
    [Area("HRMPanel")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
