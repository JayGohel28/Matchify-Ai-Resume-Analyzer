using MatchifyAi.Models;
using MatchifyAI.Data;
using MatchifyAI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatchifyAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult AdminMainForm()
        {
            return View();
        }
        public IActionResult RecruiterMainForm()
        {
            return View();
        }
        public IActionResult UserMainForm()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

    }
}
