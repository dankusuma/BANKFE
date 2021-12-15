using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BANKFE.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;
        public IActionResult Index()
        {
            return View();
        }
        public RegisterController(HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult DoRegister([FromBody] User user)
        {
            var result = _httpservices.PostData(_configuration["APIUrl"] + "/User/Add", user);
            return Ok(result.Result);
        }
    }
}
