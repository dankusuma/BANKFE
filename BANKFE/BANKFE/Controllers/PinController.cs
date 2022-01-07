using BANKFE.Models;
using BANKFE.Services;
using BANKFE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoCreatePIN([FromBody] PinViewModel param)
        {
            PinModel createPin = new PinModel(param.Token, param.UserName, param.NewPin, param.ConfirmPin);
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/User/CreatePIN", createPin);
            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }
            return Ok(result.Content.ReadAsStringAsync().Result);
        }

        [HttpPost]
        public async Task<IActionResult> DoChangePIN([FromBody] PinViewModel param)
        {
            PinModel changePin = new PinModel(param.Token, param.UserName, param.NewPin, param.ConfirmPin);
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/User/ChangePIN", changePin);
            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }
            return Ok(result.Content.ReadAsStringAsync().Result);
        }
    }
}
