using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplicationProject.Models;

public class InstructorsController : Controller
{
    private readonly WebsiteContext _context;

    public InstructorsController(WebsiteContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var instructors = _context.Instructors.ToList(); // Or use any filtering logic here
        return View(instructors); // Pass the list to the View
    }


    [HttpPost]
    public IActionResult Delete(int id)
    {
        var instructor = _context.Instructors
                                  .Include(i => i.InstructorNavigation) // Include the User data
                                  .FirstOrDefault(i => i.InstructorId == id);

        if (instructor != null)
        {
            // Remove the instructor (which will also delete the related user due to cascading delete)
            _context.Instructors.Remove(instructor);

            // Optionally, delete the related User record if needed (cascading should handle this)
            _context.Users.Remove(instructor.InstructorNavigation);

            // Save changes to the context (this deletes both the instructor and the user from the database)
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult EditInfo()
    {
        // Get the instructor ID from the session
        var instructorId = HttpContext.Session.GetInt32("UserId");

        if (instructorId == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if not found
        }

        var instructor = _context.Instructors
            .FirstOrDefault(i => i.InstructorId == instructorId);

        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }

    // Edit POST: Instructors/EditInfo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditInfo(Instructor instructor)
    {
        // Debug log or breakpoint here to see the posted instructor object
        if (instructor == null)
        {
            ModelState.AddModelError("", "Instructor data was not posted correctly.");
        }
        else
        {
            // You can also check individual properties
            if (string.IsNullOrEmpty(instructor.Name))
            {
                ModelState.AddModelError("Name", "Name cannot be empty.");
            }
        }

        // Session check
        var instructorId = HttpContext.Session.GetInt32("UserId");
        if (instructorId == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if session is invalid
        }

        if (!ModelState.IsValid)
        {
            try
            {
                _context.Update(instructor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Instructors.Any(i => i.InstructorId == instructor.InstructorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("InstructorHome","Home");
        }

        return View(instructor);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddLearningPath(int learnerId, int profileId, decimal completionStatus, string customContent, string adaptiveRules)
    {
        try
        {
            // Call the stored procedure to add the learning path
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC NewPath @LearnerID = {0}, @ProfileID = {1}, @completion_status = {2}, @custom_content = {3}, @adaptive_rules = {4}",
                learnerId, profileId, completionStatus, customContent, adaptiveRules);

            TempData["SuccessMessage"] = "Learning path added successfully!";
            return RedirectToAction("AddLearningPath");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while adding the learning path. Please try again.";
            return View();
        }
    }
    public async Task<IActionResult> AddLearningPath()
    {
        var instructorId = HttpContext.Session.GetInt32("UserId");

        if (instructorId == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if session is missing
        }

        // Fetch learners via the courses taught by the instructor
        var learners = await _context.Learners
            .Distinct()
            .ToListAsync();

        ViewBag.Learners = learners;

        return View();
    }


}
