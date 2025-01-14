using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using WebApplicationProject.Extensions;

namespace WebApplicationProject.Controllers
{
    public class LearnersController : Controller
    {
        private readonly WebsiteContext _context;

        public LearnersController(WebsiteContext context)
        {
            _context = context;
        }

        // Action to display all learners
        public async Task<IActionResult> Index()
        {
            var learners = await _context.Learners
                .Include(l => l.SidNavigation)
                .ToListAsync();
            return View(learners);
        }
        
        // POST: Learner/EditInfo
       
      




        // Action to delete a learner
        public IActionResult Delete(int id)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var userss = _context.Users.FirstOrDefault(u => u.Id == userid);
            var learner = _context.Learners
                                  
                                  .FirstOrDefault(l => l.Sid == userid);

            if (learner != null)

            {
                _context.Users.Remove(userss);
                _context.Learners.Remove(learner);
                if (learner.SidNavigation != null)
                {
                    _context.Users.Remove(learner.SidNavigation);
                }
                _context.SaveChanges();
                
            }

            return RedirectToAction("Index", "Login");
        }
       
    public async Task<IActionResult> LearningPaths()
        {
            var learnerId = HttpContext.Session.GetInt32("UserId");

            if (learnerId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if no session or user logged in
            }

            // Fetch learning paths based on the learner's ID
            var learningPaths = await _context.LearningPaths
                                              .Where(lp => lp.Sid == learnerId)
                                              .ToListAsync();

            return View(learningPaths); // Pass the learning paths to the view
        }



        public IActionResult AddGoal()
        {
            return View();
        }

        public IActionResult Quests()
        {
            // Logic to get quests for the learner, you can add more functionality later
            return View();
        }
        public async Task<IActionResult> QuestActions()
        {
            var learnerId = (int)HttpContext.Session.GetInt32("UserId");  // Assuming you have a way to get the current learner's SID

            var collaborativeQuests = await _context.CollaborativeQuests
                .Include(cq => cq.Quest)
                .Where(cq => cq.MaxParticipants > cq.Quest.LearnersCollaborations.Count) // Check if there are available spots
                .ToListAsync();

            var activeQuests = await _context.LearnersCollaborations
                .Where(lc => lc.Sid == learnerId)
                .ToListAsync();

            var achievements = await _context.Acheivements
                .Where(a => a.Sid == learnerId)
                .ToListAsync();

            var viewModel = new QuestActionsViewModel
            {
                CollaborativeQuests = collaborativeQuests,
                ActiveQuests = activeQuests,
                Achievements = achievements
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> JoinQuest(int questId)
        {   
            int iddd = (int)HttpContext.Session.GetInt32("UserId");
            var learner = await _context.Learners.FirstOrDefaultAsync(l => l.Sid == iddd);
            var quest = await _context.Quests.FindAsync(questId);

            if (quest == null)
            {
                return NotFound();
            }

            // Check if the learner is already participating in the quest
            var existingParticipation = await _context.LearnersCollaborations
                .FirstOrDefaultAsync(lc => lc.QuestId == questId && lc.Sid == learner.Sid);

            if (existingParticipation != null)
            {
                return RedirectToAction("QuestActions"); // Already joined
            }

            // Add learner to the collaborative quest
            var collaboration = new LearnersCollaboration
            {
                QuestId = questId,
                Sid = learner.Sid,
                CompletetionStatus = "incomplete"
            };

            _context.LearnersCollaborations.Add(collaboration);
            await _context.SaveChangesAsync();

            return RedirectToAction("QuestActions");
        }
        public async Task<IActionResult> ViewQuestParticipants(int questId)
        {
            // Fetch the collaborative quest based on the questId
            var collaborativeQuest = await _context.CollaborativeQuests
                .Include(cq => cq.Quest)
                .FirstOrDefaultAsync(cq => cq.QuestId == questId);

            if (collaborativeQuest == null)
            {
                return NotFound();
            }

            // Fetch the participants of the quest (learners)
            var participants = await _context.LearnersCollaborations
                .Where(lc => lc.QuestId == questId)
                .Include(lc => lc.SidNavigation)  // Assuming Learner is a related model
                .ToListAsync();

            // Create a view model to pass the data
            var viewModel = new QuestParticipantsViewModel
            {
                Quest = collaborativeQuest.Quest,
                Participants = participants.Select(lc => lc.SidNavigation).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ViewProfile()
        {
            var learnerId = HttpContext.Session.GetInt32("UserId");

            if (learnerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var learner = await _context.Learners
                .Include(l => l.SidNavigation)
                .SingleOrDefaultAsync(l => l.Sid == learnerId);

            if (learner == null)
            {
                return NotFound();
            }

            // Get the image URL from session
            var profileImageUrl = HttpContext.Session.GetString("ProfileImageUrl") ?? "/uploads/profile-images/default.png"; // Default image if none uploaded

            var model = new LearnerProfileTempModel
            {
                Fname = learner.Fname,
                Lname = learner.Lname,
                Country = learner.Country,
                Email = learner.SidNavigation.Email,
                BirthDate = learner.BirthDate,
                ProfileImageUrl = profileImageUrl
            };

            return View(model);
        }

        // Action to save the profile
        [HttpPost]
        public async Task<IActionResult> SaveProfile(LearnerProfileTempModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload if provided
                if (model.ProfileImage != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(model.ProfileImage.FileName);
                    var fileExtension = Path.GetExtension(model.ProfileImage.FileName);
                    var newFileName = $"{Guid.NewGuid()}{fileExtension}";

                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profile-images");

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    var filePath = Path.Combine(uploadFolder, newFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfileImage.CopyToAsync(stream);
                    }

                    // Store the image URL temporarily
                    model.ProfileImageUrl = $"/uploads/profile-images/{newFileName}";
                }

                // Save the temporary profile image URL and other data (no database interaction here)
                // Returning to the profile view with the updated temporary model
                return RedirectToAction("ViewProfile");
            }

            return View(model);
        }

        // Optionally: Add a method to view learner's profile by ID if needed
        public async Task<IActionResult> ViewProfileById(int learnerId)
        {
            var learner = await _context.Learners
                .Include(l => l.SidNavigation)
                .SingleOrDefaultAsync(l => l.Sid == learnerId);

            if (learner == null)
            {
                return NotFound();
            }

            var tempModel = new LearnerProfileTempModel
            {
                Fname = learner.Fname,
                Lname = learner.Lname,
                BirthDate = learner.BirthDate,
                Country = learner.Country,
                Email = learner.SidNavigation.Email,
                ProfileImageUrl = "/uploads/profile-images/default.png" // Or set the default URL
            };

            return View("ViewProfile", tempModel);  // Render the view with the learner's data
        }
        // GET: EditInfo - Show the Edit Profile form
        [HttpGet]
        public IActionResult EditInfo()
        {
            var learnerId = HttpContext.Session.GetInt32("UserId"); // Get the learner's ID from session

            if (learnerId == null)
            {
                return RedirectToAction("Login", "Account"); // If the learner is not logged in, redirect to login
            }

            // Fetch learner data by ID
            var learnerInfo = _context.Learners
                .Include(l => l.SidNavigation) // If needed for related data
                .FirstOrDefault(l => l.Sid == learnerId); // Fetch learner data

            if (learnerInfo == null)
            {
                return NotFound(); // Return NotFound if the learner does not exist
            }

            // Map the data to the temporary model
            var learner = new LearnerProfileTempModel
            {
                Fname = learnerInfo.Fname,
                Lname = learnerInfo.Lname,
                Country = learnerInfo.Country,
                Email = learnerInfo.SidNavigation.Email,
                BirthDate = learnerInfo.BirthDate
            };

            return View(learner); // Return the view with the temporary model
        }

        // POST: Learners/EditInfo - Handle form submission and image upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(LearnerProfileTempModel model)
        {
            var learnerId = HttpContext.Session.GetInt32("UserId");

            if (learnerId != null)
            {
                // Check if an image is uploaded
                if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    try
                    {
                        // Generate a unique file name for the uploaded image
                        var fileName = $"{learnerId}_{Guid.NewGuid()}{Path.GetExtension(model.ProfileImage.FileName)}";

                        // Set the folder path where images will be stored
                        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profile-images");

                        // Ensure the directory exists
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        // Set the full file path
                        var filePath = Path.Combine(uploadFolder, fileName);

                        // Save the uploaded image to the folder
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProfileImage.CopyToAsync(stream);
                        }

                        // Store the image URL in the session (without modifying the database)
                        HttpContext.Session.SetString("ProfileImageUrl", $"/uploads/profile-images/{fileName}");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Error saving profile image. Please try again.");
                        // Log the exception (for debugging purposes)
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                // Now you can update the learner's data in the database (without saving the image URL)
                var learnerToUpdate = await _context.Learners
                    .Include(l => l.SidNavigation)
                    .SingleOrDefaultAsync(l => l.Sid == learnerId);

                if (learnerToUpdate != null)
                {
                    learnerToUpdate.Fname = model.Fname;
                    learnerToUpdate.Lname = model.Lname;
                    learnerToUpdate.Country = model.Country;
                    learnerToUpdate.SidNavigation.Email = model.Email;
                    learnerToUpdate.BirthDate = model.BirthDate;

                    // Save the other changes to the database (excluding the image URL)
                    await _context.SaveChangesAsync();

                    TempData["ProfileUpdatedMessage"] = "Your profile has been updated successfully!";
                    return RedirectToAction("ViewProfile");
                }
            }

            // If model is not valid, return the form again
            return View(model);
        }
    }







}

