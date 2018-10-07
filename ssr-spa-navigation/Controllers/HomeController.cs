using Microsoft.AspNetCore.Mvc;
using ssr_spa_navigation.Infrastructure;

namespace ssr_spa_navigation.Controllers
{
    public class HomeController : BaseController
    {
        [ServiceFilter(typeof(DefaultStructureResult))]
        public IActionResult Index()
        {
            return ResolveResult();
        }
    }
}
