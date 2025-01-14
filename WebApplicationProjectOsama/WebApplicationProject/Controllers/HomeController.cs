using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebsiteContext _context;

        public HomeController(WebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult InstructorHome()
        {
            // Retrieve the instructor ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Redirect to login page if the user is not authenticated
                return RedirectToAction("Index", "Login");
            }

            // Retrieve the instructor from the database using the userId
            var instructor = _context.Instructors
                .Include(i => i.InstructorNavigation)  // Assuming 'InstructorNavigation' is a related entity, like user data
                .Include(i => i.PathReviews)           // Include related courses (if needed)
                .Include(i => i.DisscussionForums)     // Include related discussion forums
                .FirstOrDefault(i => i.InstructorId == userId);

            // If the instructor is not found, return a NotFound error
            if (instructor == null)
            {
                return NotFound(); // Instructor not found
            }

            
            return View(instructor);
        }

        public IActionResult AdminHome()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            var adminn = _context.Admins
                            .Include(a => a.AidNavigation).FirstOrDefault(a => a.Aid == userId);
            ViewData["Title"] = "Admin Home";
            //ViewData["Title"] = "Admin Home"; // Set ViewData["Title"]
            Console.WriteLine($"ViewData['Title']: {ViewData["Title"]}"); // Debug log
            return View(adminn);
        }
        public async Task<IActionResult> LearnerHome()
        {
            // Retrieve learner ID from the session
            var learnerId = HttpContext.Session.GetInt32("UserId");
            if (learnerId == null)
            {
                return RedirectToAction("Index", "Login"); // Redirect to login if not authenticated
            }

            // Retrieve the learner by ID, including personalization profiles
            var learner = await _context.Learners
                .Include(l => l.PersonalizationProfiles)  // Ensure we get the personalization profiles for the learner
                .FirstOrDefaultAsync(l => l.Sid == learnerId);

            if (learner == null)
            {
                return NotFound(); // Return a 404 page if learner is not found
            }

            ViewData["Title"] = "Learner Home"; // Set the page title
            return View(learner); // Pass the learner object to the view
        }


    }





}



