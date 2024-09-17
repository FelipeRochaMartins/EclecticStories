using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Base
{
    public class BaseController : Controller
    {
        public void PopUpInfo(string content)
        {
            ViewData["info"] = "info";
            ViewData["content"] = content;
        }

        public void PopUpWarning(string content)
        {
            ViewData["warning"] = "warning";
            ViewData["content"] = content;
        }

        public void PopUpError(string content)
        {
            ViewData["error"] = "error";
            ViewData["content"] = content;
        }

        public void PopUpSucess(string content)
        {
            ViewData["success"] = "success";
            ViewData["content"] = content;
        }
    }
}
