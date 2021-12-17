using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;
        public LoginController( HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> DoLogin([FromBody] Login login)
        {
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/User/Authenticate", login);
            return Ok(!string.IsNullOrWhiteSpace(result));
        }
    }
}
