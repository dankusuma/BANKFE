using BANKFE.Models.ChangeData;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BANKFE.Controllers
{
    public class SettingController : Controller
    {
        private readonly HttpService _httpservices;
        private readonly IConfiguration _configuration;

        public SettingController(HttpService httpservices, IConfiguration configuration)
        {
            _httpservices = httpservices;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var user = User as ClaimsPrincipal;
            string username = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();

            ViewData["username"] = username;
            ViewBag.Occupations = new List<SelectListItem>()
            {
                new SelectListItem {Text="PNS", Value="PNS"},
                new SelectListItem {Text="Polri", Value="Polri"},
                new SelectListItem {Text="Karyawan", Value="Karyawan"},
                new SelectListItem {Text="Wiraswasta", Value="Wiraswasta"},
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAddress([Bind("ADDRESS", "KELURAHAN", "KECAMATAN", "KABUPATEN_KOTA", "PROVINCE")] ChangeData changeAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(changeAdd);
            }

            var user = User as ClaimsPrincipal;
            string username = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            changeAdd.USERNAME = username;
            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/ChangeData/ChangeAddress", changeAdd);

            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }
            return RedirectToAction("Index", "Setting");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeJob([Bind("JOB")] ChangeData change)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(change);
            }

            var user = User as ClaimsPrincipal;
            string username = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            change.USERNAME = username;

            var result = await _httpservices.PostData(_configuration["APIUrl"] + "/ChangeData/ChangeJob", change);

            if ((int)result.StatusCode != 200)
            {
                return Unauthorized(result.Content.ReadAsStringAsync().Result);
            }
            return RedirectToAction("Index", "Setting");
        }
    }
}
