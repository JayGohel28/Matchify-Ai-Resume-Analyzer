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

        // ================= INDEX =================
        public IActionResult Index()
        {
            return View();
        }

        // ================= CONTACT =================
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

        // ================= USER RESUME FORM =================

        // GET
        public IActionResult UserMainForm()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserMainForm(IFormFile ResumeFile, string JobRole, string ExperienceLevel, string EmploymentType)
        {
            if (ResumeFile == null || ResumeFile.Length == 0)
            {
                ModelState.AddModelError("", "Please upload resume.");
                return View();
            }

            string resumeText = "";

            using (var reader = new StreamReader(ResumeFile.OpenReadStream()))
            {
                resumeText = reader.ReadToEnd();
            }

            // ===== Role Based Required Skills =====
            Dictionary<string, List<string>> roleSkills = new()
            {
                { ".NET Developer", new List<string>{ "c#", "asp.net", "sql", "entity framework", "mvc" } },
                { "Java Developer", new List<string>{ "java", "spring", "hibernate", "sql" } },
                { "Python Developer", new List<string>{ "python", "django", "flask", "sql" } },
                { "Full Stack Developer", new List<string>{ "html", "css", "javascript", "react", "node", "sql" } }
            };

            var requiredSkills = roleSkills.ContainsKey(JobRole)
                ? roleSkills[JobRole]
                : new List<string>();

            List<string> extractedSkills = new();
            List<string> missingSkills = new();

            string lowerResume = resumeText.ToLower();

            foreach (var skill in requiredSkills)
            {
                if (lowerResume.Contains(skill))
                    extractedSkills.Add(skill);
                else
                    missingSkills.Add(skill);
            }

            int score = 0;
            if (requiredSkills.Count > 0)
                score = (extractedSkills.Count * 100) / requiredSkills.Count;

            // Detect experience
            string experienceDetected = lowerResume.Contains("year")
                ? "Experience Mentioned"
                : "Not Clearly Mentioned";

            // Pass data to Result View
            ViewBag.Score = score;
            ViewBag.ExtractedSkills = extractedSkills;
            ViewBag.MissingSkills = missingSkills;
            ViewBag.JobRole = JobRole;
            ViewBag.ExperienceDetected = experienceDetected;
            ViewBag.Suggestions = missingSkills.Count > 0
                ? "Improve your resume by adding missing technical skills and project experience."
                : "Strong profile. Focus on measurable achievements.";

            return View("Result");
        }

        // ================= RECRUITER =================

        public IActionResult RecruiterMainForm()
        {
            return View();
        }

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

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
