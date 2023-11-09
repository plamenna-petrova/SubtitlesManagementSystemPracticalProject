using Microsoft.AspNetCore.Mvc;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToIndexActionInCurrentController()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
