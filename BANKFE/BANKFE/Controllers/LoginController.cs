using BANKFE.Models;
using BANKFE.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> DoLoginAsync([FromBody] Login login)
        {
            var result = _httpservices.PostData(_configuration["APIUrl"] + "/User/Authenticate", login);
            if(result.Result.Code != 200)
            {
                return Unauthorized(result.Result.Message);
            }
            var token = new JwtSecurityToken(jwtEncodedString: result.Result.Message);
            var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name,token.Claims.First(c => c.Type == ClaimTypes.Name).Value),
                        new Claim(ClaimTypes.Role, token.Claims.First(c => c.Type == ClaimTypes.Role).Value),
                        new Claim(ClaimTypes.Authentication, result.Result.Message)

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            var principal = new ClaimsPrincipal(identity);
            //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes((token.ValidTo - token.ValidFrom).TotalMinutes),
                IsPersistent = true
            }) ;
            return Ok();
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }


    }
}
