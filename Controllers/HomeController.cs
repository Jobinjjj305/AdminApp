using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using StudentApp.Data;
using StudentApp.Models;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace StudentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentAppContext _context;
        private readonly StudentAppContext _dbContext;
       
        public HomeController(ILogger<HomeController> logger, StudentAppContext context, StudentAppContext dbContext)
        {
            _logger = logger;
            _context = context;
            _dbContext = dbContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                // Validate user credentials against your database using injected context (_context)
                bool isValidUser = _context.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
                bool ischecked = user.Ischecked;
                if (isValidUser)
                {
                    CookieOptions option = new CookieOptions();
                    // Check if 'rememberMe' cookie exists
                    if (ischecked==true)
                    {

                        option.Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        option.Expires = DateTime.MinValue;
                    }
                    //Response.Cookies.Append("Username", user.Username, option);
                    HttpContext.Session.SetString("Password", user.Password);
                    HttpContext.Session.SetString("Username", user.Username);
                    // Implement your login logic here (e.g., set session variables)
                    return RedirectToAction("Index", "Home"); // Redirect to homepage after successful login
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View("Index");
        }
        [HttpPost]
        public IActionResult Register(UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                _dbContext.tbl_UserRegistration.Add(userRegistration);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
           
            TempData["Success"] = "Added Successfully!";
            return RedirectToAction("Index", "Home");
            //TempData["SuccessMessage"] 
           // return View("Register", userRegistration);
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("Username");

            // Pass the username to the view
            ViewData["Username"] = username;

            return View();
        }
        public IActionResult Login()
        {
            // Retrieve values from cookies
            //string username = Request.Cookies["Username"].Value;
            string username = HttpContext.Session.GetString("Username");
            string password = HttpContext.Session.GetString("Password");
            var model = new User
            {
                Username = username,
                Password = password
            };

         
            
           
            return View(model);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Error_404()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Frequently_Asked_Questions()
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
