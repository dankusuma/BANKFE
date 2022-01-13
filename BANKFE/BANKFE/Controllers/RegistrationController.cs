using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SubmitRegistration([FromBody] Registration model)
        {
            var user =  await _httpservices.PostData(_configuration["APIUrl"] + "/User/Add", model);
            if (user.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(user.StatusCode);
            }
            else {
                return BadRequest(user.Content);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUpload([FromBody] Upload upload)
        {
            var taskupload = await _httpservices.PostData(_configuration["APIUrl"] + "/User/Upload", upload);
            if (taskupload.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(taskupload.StatusCode);
            }
            else
            {
                return BadRequest(taskupload.Content);
            }
        }
    }
}
