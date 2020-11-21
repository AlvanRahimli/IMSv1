using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IMSv1.Models.Dtos;
using IMSv1.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        
        // GET Login
        public IActionResult Login()
        {
            return View();
        }
        
        // POST Login
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginCreds)
        {
            var user = await _repo.Login(loginCreds);
            if (user == null)
            {
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.Now
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return View("Profile", user);
        }
    }
}