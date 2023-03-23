using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.ViewComponents
{
    public class NotificationsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
