using MatchifyAI.Data;
using MatchifyAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MatchifyAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(model);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Feedback sent successfully!";

                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // ================= RECRUITER JOB POST =================

        // GET: Recruiter Form
        public IActionResult RecruiterMainForm()
        {
            return View();
        }

        // POST: Recruiter Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecruiterMainForm(Job model)
        {
            if (ModelState.IsValid)
            {
                _context.Jobs.Add(model);
                _context.SaveChanges();

                TempData["JobMessage"] = "Job posted successfully!";
                return RedirectToAction("RecruiterMainForm");
            }

            return View(model);
        }

        // =======================================================

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
