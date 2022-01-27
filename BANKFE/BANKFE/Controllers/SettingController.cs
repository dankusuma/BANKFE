using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            var user = User as ClaimsPrincipal;
            string username = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();

            ViewData["username"] = username;

            return View();
        }
    }
}
