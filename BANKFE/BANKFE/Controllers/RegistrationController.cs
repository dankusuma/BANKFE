using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BANKFE.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;

        public RegistrationController(HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRegistration([FromBody] Registration model, [FromBody] Upload upload)
        {
            var result = _httpservices.PostData(_configuration["APIUrl"] + "/User/Add", model);
            var result2 = _httpservices.PostData(_configuration["APIUrl"] + "/User/Upload", upload); // FE kirim string foto / video yang diconvert jadi Base64 string
            return Ok(result.Result);
        }
    }
}
