using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class PersonalizationController : Controller
    {
        private readonly WebsiteContext _context;

        public PersonalizationController(WebsiteContext context)
        {
            _context = context;
        }

        // Admin: Display all personalization profiles
        public async Task<IActionResult> Index()
        {
            var profiles = await _context.PersonalizationProfiles.Include(p => p.SidNavigation).ToListAsync();
            return View(profiles);
        }

        // Learner: Display only logged-in learner's profiles
        public async Task<IActionResult> LearnerProfiles()
        {
            var learnerId = HttpContext.Session.GetInt32("UserId");
            if (learnerId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var learner = await _context.Learners
                .Include(l => l.PersonalizationProfiles)
                .FirstOrDefaultAsync(l => l.Sid == learnerId);

            if (learner == null)
            {
                return NotFound();
            }

            return View(learner.PersonalizationProfiles);
        }

        // Admin: Delete any personalization profile
        [HttpPost]
        public async Task<IActionResult> Delete(int sid, int orderNumber)
        {
            var profile = await _context.PersonalizationProfiles
                .FirstOrDefaultAsync(p => p.Sid == sid && p.OrderNumber == orderNumber);

            if (profile == null)
            {
                return NotFound();
            }

            _context.PersonalizationProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirect to the admin list
        }

        // Learner: Delete only their own personalization profile
        [HttpPost]
        public async Task<IActionResult> LearnerDelete(int orderNumber)
        {
            var learnerId = HttpContext.Session.GetInt32("UserId");
            if (learnerId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var profile = await _context.PersonalizationProfiles
                .FirstOrDefaultAsync(p => p.Sid == learnerId && p.OrderNumber == orderNumber);

            if (profile == null)
            {
                return NotFound();
            }

            _context.PersonalizationProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(LearnerProfiles)); // Redirect to the learner's profiles
        }
    }
}
