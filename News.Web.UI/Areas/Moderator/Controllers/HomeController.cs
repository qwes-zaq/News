using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Moderator.Controllers
{
    public class HomeController : Controller
    {
        [Area("Moderator")]
        [Authorize(Roles = "Moderator")]
        [Route("Moderator/[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
