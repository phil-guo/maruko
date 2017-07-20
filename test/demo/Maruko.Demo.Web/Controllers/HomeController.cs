using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maruko.Demo.Application;
using Maruko.Demo.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _app;

        public HomeController(IUserAppService app)
        {
            _app = app;
        }

        public IActionResult Index()
        {
            _app.Insert(new CreateUserDto());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
