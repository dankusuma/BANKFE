using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BANKFE.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly HttpService _httpservices;

        public LoginController( HttpService httpservice)
        {
            _httpservices = httpservice;
        }

        [HttpPost]
        public IActionResult DoLogin([FromBody] Login login)
        {
            var result = _httpservices.PostData("https://localhost:44321/User/Authenticate", login);
            return Ok(result.Result);
        }

    }
}
