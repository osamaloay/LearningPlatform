using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class DiscussionRoomsController : Controller
    {
        private readonly WebsiteContext _context;
        private readonly ILogger<DiscussionRoomsController> _logger;

        public DiscussionRoomsController(WebsiteContext context)
        {
            _context = context;
        }

        // Action to display all the discussion forums for a learner based on their enrolled courses
        public async Task<IActionResult> GetForums()
        {
            // Retrieve the user ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized(); // User is not logged in
            }

            // Check if the user is a learner
            var learner = await _context.Learners
                .Include(l => l.Enrollments)
                    .ThenInclude(e => e.CidNavigation) // Include course details
                .FirstOrDefaultAsync(l => l.Sid == userId);

            if (learner != null)
            {
                // Learner is logged in; get forums for the courses they're enrolled in
                var enrolledCoursesIds = learner.Enrollments
                    .Select(e => e.CidNavigation.Cid)
                    .ToList();

                var forums = await _context.DisscussionForums
                    .Where(f => enrolledCoursesIds.Contains(f.Cid)) // Match forums by course IDs
                    .ToListAsync();

                return View(forums); // Pass forums to the view
            }

            // Check if the user is an instructor
            var instructor = await _context.Instructors
                .Include(i => i.Cids) // Include courses taught by the instructor
                .FirstOrDefaultAsync(i => i.InstructorId == userId);

            if (instructor != null)
            {
                // Instructor is logged in; get forums for the courses they teach
                var taughtCoursesIds = instructor.Cids
                    .Select(c => c.Cid)
                    .ToList();

                var forums = await _context.DisscussionForums
                    .Where(f => taughtCoursesIds.Contains(f.Cid)) // Match forums by course IDs
                    .ToListAsync();

                return View(forums); // Pass forums to the view
            }

            // If the user is neither a learner nor an instructor
            return Unauthorized();
        }


        public async Task<IActionResult> ForumRoom(int forumId)
        {
            // Retrieve the learnerId (user's ID) from the session
            var learnerId = HttpContext.Session.GetInt32("UserId");

            // Fetch the specific forum along with its discussions (posts)
            var forum = await _context.DisscussionForums
                .Include(f => f.LearnerDisscussions)
                    .ThenInclude(ld => ld.SidNavigation) // Use 'User' to get the details of the user who posted
                .FirstOrDefaultAsync(f => f.ForumId == forumId);

            if (forum == null)
            {
                return NotFound();
            }

            // Filter the discussions related to this forum
            var learnerDiscussion = forum.LearnerDisscussions.ToList();

            // Pass forum details and the learner discussion posts to the view
            return View(new ForumViewModel
            {
                Forum = forum,
                LearnerDiscussions = learnerDiscussion,
                LearnerId = learnerId
            });
        }


        // Action to post a new message to the discussion forum
        [HttpPost]
        [ValidateAntiForgeryToken] 
       public async Task<IActionResult> Posting(int forumId, string postContent)
        {
            if (string.IsNullOrWhiteSpace(postContent))
            {
                TempData["Error"] = "Message cannot be empty.";
                return RedirectToAction(nameof(ForumRoom), new { forumId });
            }

            // Retrieve the learnerId from the session
            int? sessionLearnerId = HttpContext.Session.GetInt32("UserId");
            if (!sessionLearnerId.HasValue)
            {
                TempData["Error"] = "User session expired. Please log in again.";
                return RedirectToAction("Login", "Account"); // Adjust based on your login flow
            }

            byte learnerId = (byte)sessionLearnerId.Value;

            // Validate forum existence
            var forum = await _context.DisscussionForums.FindAsync(forumId);
            if (forum == null)
            {
                return NotFound($"Forum with ID {forumId} does not exist.");
            }

            // Create the new discussion post
            var newDiscussion = new LearnerDisscussion
            {
                ForumId = forumId,
                Sid = learnerId,
                Post = postContent,
                TimePosted = DateTime.Now
            };

            // Save to the database
            try
            {
                _context.LearnerDisscussions.Add(newDiscussion);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while saving your post.";
                Console.WriteLine($"Error posting message: {ex.Message}");
            }

            // Redirect back to the forum room to display the updated discussion
            return RedirectToAction(nameof(ForumRoom), new { forumId });
        }
    

   
        [HttpPost]
        public async Task<IActionResult> DeleteForum(int id)
        {
            var forum = await _context.DisscussionForums.FindAsync(id);
            if (forum == null)
            {
                return NotFound();
            }

            _context.DisscussionForums.Remove(forum);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetForums");
        }
        public IActionResult CreateForum()
        {
            return View();
        }
        // POST: CreateForum
        [HttpPost]
        public IActionResult CreateForum(int moduleId, int courseId, string title, string description)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                TempData["ErrorMessage"] = "Title and Description are required.";
                return View();
            }

            try
            {
                // Prepare parameters for stored procedure
                var moduleIdParam = new SqlParameter("@moduleID", SqlDbType.Int) { Value = moduleId };
                var courseIdParam = new SqlParameter("@courseID", SqlDbType.Int) { Value = courseId };
                var titleParam = new SqlParameter("@title", SqlDbType.VarChar, 50) { Value = title };
                var descriptionParam = new SqlParameter("@description", SqlDbType.VarChar, 50) { Value = description };

                // Execute stored procedure
                _context.Database.ExecuteSqlRaw("EXEC CreateDiscussion @moduleID, @courseID, @title, @description",
                    moduleIdParam, courseIdParam, titleParam, descriptionParam);

                // Redirect to another page (e.g., home page)
                return RedirectToAction("InstructorHome", "Home");
            }
            catch (SqlException)
            {
                // Handle SQL exceptions by displaying an error message
                TempData["ErrorMessage"] = "module id or course id does not exists";
                return View(); // Return to the form with the error message
            }
            catch (Exception)
            {
                // Catch any other unexpected errors
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return View(); // Return to the form with the error message
            }
            
        }



    }


    // ViewModel for passing forum and learner discussions
    public class ForumViewModel
    {
        public DisscussionForum Forum { get; set; }
        public List<LearnerDisscussion> LearnerDiscussions { get; set; }
        public int? LearnerId { get; set; }
        public bool IsInstructor { get; set; }
        public bool IsLearner { get; set; }
    }
}
