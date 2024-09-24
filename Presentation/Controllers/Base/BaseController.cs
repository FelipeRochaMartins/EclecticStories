using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Base
{
    public class BaseController : Controller
    {
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public void PopUpInfo(string content)
        {
            TempData["info"] = "info";
            TempData["content"] = content;
        }

        public void PopUpWarning(string content)
        {
            TempData["warning"] = "warning";
            TempData["content"] = content;
        }

        public void PopUpError(string content)
        {
            TempData["error"] = "error";
            TempData["content"] = content;
        }

        public void PopUpSuccess(string content)
        {
            TempData["success"] = "success";
            TempData["content"] = content;
        }
    }
}
