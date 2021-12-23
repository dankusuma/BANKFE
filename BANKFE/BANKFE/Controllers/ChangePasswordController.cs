using BANKFE.Services;
using BANKFE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class ChangePasswordController : Controller
    {

        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;
        public ChangePasswordController(HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DoChangePassword([FromBody] ChangePasswordViewModel param)
        {
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/User/Authenticate", param);
            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync());
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SendChangePassword([FromQuery] string email)
        {
            var result = await _httpservices.GetData(_configuration["APIUrl"] + "/User/Authenticate");
            return Ok();
        }


    }
}
