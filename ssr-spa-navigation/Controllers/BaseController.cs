using Microsoft.AspNetCore.Mvc;

namespace ssr_spa_navigation.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ResolveResult(object data = null)
        {
            var nameTokenValue = (string)RouteData.DataTokens["Name"];

            if (nameTokenValue != "default_api")
            {
                return View("../Home/Index", data);
            }

            return Ok(data);
        }
    }
}
