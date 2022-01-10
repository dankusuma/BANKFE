using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, HttpService httpservices, IConfiguration configuration)
        {
            _logger = logger;
            _httpservices = httpservices;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = User as ClaimsPrincipal;
            string username = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            string pinStatus = DoPINStatus(username).Result;

            ViewData["username"] = username;

            if (pinStatus == "true") return View();  /// Redirect to dashboard
            else return RedirectToAction("Index", "Pin", new { mode = "create", username = username });   /// Redirect to PIN page
        }

        public IActionResult NotReadyPage()
        {
            return View("NotReady");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<string> DoPINStatus(string username)
        {
            string status = "false";
            PinModel changePin = new PinModel("", username, "", "");
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/User/PINStatus", changePin);
            if ((int)result.StatusCode == 200)
            {
                return status = result.Content.ReadAsStringAsync().Result;
            }
            return status;
        }
    }
}
