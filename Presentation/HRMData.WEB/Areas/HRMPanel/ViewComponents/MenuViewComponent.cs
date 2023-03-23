using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
