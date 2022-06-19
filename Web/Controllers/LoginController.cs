using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly FlightManagerDbContext _context;
        private readonly LoginContext loginContext;
        public static User loggedUser;

        public LoginController(FlightManagerDbContext context)
        {
            _context = context;
            loginContext = new LoginContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Login(string username, string password)
        {
            User tempUser = (await loginContext.Login(username, password));
            if (tempUser != null)
            {
                loggedUser = tempUser;
                return Json(new { result = "Success" });
            }
            return Json(new { result = "Failure" });
        }

        public IActionResult Logout()
        {
            loggedUser = null;
            return RedirectToAction("Index", "Flight");
        }

        public static bool UserLogged()
        {
            return loggedUser != null;
        }

        public static User ReturnUser()
        {
            return loggedUser;
        }

        public static bool UserIsAdmin()
        {
            if (loggedUser != null)
            {
                return loggedUser.Role == Role.Admin;
            }
            return false;
        }
    }
}
