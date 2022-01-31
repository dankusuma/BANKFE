using BANKFE.Models;
using BANKFE.Services;
using BANKFE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Web;

namespace BANKFE.Controllers
{
    public class PinController : Controller
    {
        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;

        public PinController(HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Request.Query["username"][0];
            string mode = HttpContext.Request.Query["mode"][0];

            ViewData["mode"] = mode;
            ViewData["username"] = username;

            return View();  /// Redirect to dashboard

        }

        [HttpPost]
        public async Task<IActionResult> DoCreatePIN([FromBody] PinViewModel param)
        {
            PinModel createPin = new(param.UserName, param.NewPin);

            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/Pin/CreatePIN", createPin);

            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }

            return Ok(result.Content.ReadAsStringAsync().Result);
        }

        [HttpPost]
        public async Task<IActionResult> DoChangePIN([FromBody] PinViewModel param)
        {
            PinModel changePin = new(param.UserName, param.NewPin);

            var result = await _httpservices.PatchData(_configuration["APIUrl"] + "/Pin/ChangePIN", changePin);

            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }

            return Ok(result.Content.ReadAsStringAsync().Result);
        }

        [HttpGet]
        public async Task<string> DoPINStatus(string username)
        {
            string status = "false";

            status = await _httpservices.GetData(_configuration["APIUrl"] + "/Pin/PINStatus?username=" + username);

            return status;
        }
    }
}
