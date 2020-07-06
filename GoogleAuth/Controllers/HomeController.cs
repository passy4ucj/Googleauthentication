using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoogleAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GoogleAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string returnUrl)
        {
            UserLogin model = new UserLogin
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemeAsync()).ToList()
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new { ReturnUrl = returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        
        public async Task<IActionResult> Login(string returnUrl)
        {
            //UserLogin model = new UserLogin
            //{
            //    ReturnUrl = returnUrl,
            //    ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemeAsync()).ToList()
            //};
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
