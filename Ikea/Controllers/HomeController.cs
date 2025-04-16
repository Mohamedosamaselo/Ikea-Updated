using System.Diagnostics;
using Ikea.PL.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ikea.Controllers
{

    //[Authorize(Roles ="Admin,Customer")]// Role Admin or Customer 
    //[Authorize(AuthenticationSchemes ="Identity.Application")]// default Authentication Schema 
    //[Authorize(AuthenticationSchemes = "Hamada")]// 


    //[AllowAnonymous] // Default (NonAuthorized User )
    [Authorize( AuthenticationSchemes ="Identity.Application")]// Defualt Schema in 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[AllowAnonymous] lw 3awz allow for user to Make this action Without Token 
        public IActionResult Index()// Action 
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
