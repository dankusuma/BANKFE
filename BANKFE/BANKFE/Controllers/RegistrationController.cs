﻿using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
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
        public async Task<IActionResult> SubmitRegistration([FromBody] RegisterUpload model)
        {
            //string[] split = jsonstring.Split("splithere123!");
            //Registration model = Newtonsoft.Json.JsonConvert.DeserializeObject<Registration>(split[0]);
            //Upload upload = Newtonsoft.Json.JsonConvert.DeserializeObject<Upload>(split[1]);
            var user = await _httpservices.PostData(_configuration["APIUrl"] + "/User/Add", model.user);
            var taskupload = await _httpservices.PostData(_configuration["APIUrl"] + "/User/Upload", model.upload);

            if (user.StatusCode == System.Net.HttpStatusCode.OK && taskupload.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok("Registration Status Code: " + user.StatusCode + ", Upload Status Code: " + taskupload.StatusCode);
            }
            else
            {
                return BadRequest("Registration Content: " + user.Content + ", Upload Content: " + taskupload.Content);
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

        [HttpGet]
        public bool ValidateEmail([FromQuery] string email)
        {
            var checkemail = _httpservices.PostData(_configuration["APIUrl"] + "/User/isEmailDuplicate", new { Email = email });

            if (checkemail.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public bool ValidateUsername([FromQuery] string username)
        {
            var checkuser = _httpservices.PostData(_configuration["APIUrl"] + "/User/isUserDuplicate", new { Username = username });

            if (checkuser.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public bool ValidatePhone([FromQuery] string handphone)
        {
            var checkphone = _httpservices.PostData(_configuration["APIUrl"] + "/User/isPhoneDuplicate", new { Phone = handphone });

            if (checkphone.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public bool ValidateNIK([FromQuery] string nik)
        {
            var checknik = _httpservices.PostData(_configuration["APIUrl"] + "/User/isNIKDuplicate", new { NIK = nik });

            if (checknik.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
