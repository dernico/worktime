using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace worktime.server.Controllers
{
    public class AuthController : Controller
    {
      
        [Route("auth/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [Route("auth/external")]
        [HttpGet]
        public IActionResult External(string provider)
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "http://localhost:8080/#/todos"
            };
 
            return new ChallengeResult(provider, authProperties);
        }

        [Route("auth/logout")]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
        
            return RedirectToAction(nameof(Login));
        }
    }
}
