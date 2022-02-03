using BANKFE.Services;
using BANKFE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;

        public UserController(HttpService httpservice, IConfiguration configuration)
        {
            _httpservices = httpservice;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<UserViewModel> GetUserStatus(string username)
        {
            var status = await _httpservices.GetData(_configuration["APIUrl"] + "/User/GetUserStatus?username=" + username);

            return JsonConvert.DeserializeObject<UserViewModel>(status.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateIsActive(string username)
        {
            var result = await _httpservices.PatchData(_configuration["APIUrl"] + "/User/UpdateIsActive?username=" + username, null);

            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }

            return Ok(result.Content.ReadAsStringAsync().Result);
        }
    }
}
