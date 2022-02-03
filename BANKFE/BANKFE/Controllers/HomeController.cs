using BANKFE.Models;
using BANKFE.Services;
using BANKFE.ViewModels;
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

        private readonly UserController _userController;
        private readonly PinController _pinController;

        public HomeController(ILogger<HomeController> logger, HttpService httpservices, IConfiguration configuration)
        {
            _logger = logger;
            _httpservices = httpservices;
            _configuration = configuration;

            _userController = new(httpservices, configuration);
            _pinController = new(httpservices, configuration);
        }

        [Authorize]
        public IActionResult Index()
        {
            var userClaimPrincipal = User as ClaimsPrincipal;
            string username = userClaimPrincipal.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            var userData = GetUserStatus(username).Result;  // Get PinStatus, IsValidate & IsActive altogether

            if (userData.PinStatus == "true")
            {
                return View(userData);  /// Redirect to dashboard
            }
            else
            {
                return RedirectToAction("Index", "Pin", new { mode = "create", username = username });   /// Redirect to PIN page
            }
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
            return await _pinController.DoPINStatus(username);
        }

        [HttpGet]
        public async Task<UserViewModel> GetUserStatus(string username)
        {
            return await _userController.GetUserStatus(username);
        }
    }
}
