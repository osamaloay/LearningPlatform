using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using System.Linq;

namespace WebApplicationProject.Controllers
{
    public class QuestController : Controller
    {
        private readonly WebsiteContext _context;

        public QuestController(WebsiteContext context)
        {
            _context = context;
        }

        // Display all quests
        public IActionResult Index()
        {
            var quests = _context.Quests.ToList();
            return View(quests);
        }

        // Display quest details
        public IActionResult QuestDetails(int questId)
        {
            var quest = _context.Quests
                .Where(q => q.QuestId == questId)
                .FirstOrDefault();

            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // Add a new quest
        [HttpGet]
        public IActionResult AddQuest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuest(Quest quest)
        {
            if (ModelState.IsValid)
            {
                _context.Quests.Add(quest);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quest);
        }

        // Edit an existing quest
        [HttpGet]

        public IActionResult EditQuest(int questId)
        {
            var quest = _context.Quests
                .Where(q => q.QuestId == questId)
                .FirstOrDefault();

            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuest(Quest quest)
        {
            if (ModelState.IsValid)
            {
                _context.Quests.Update(quest);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quest);
        }

        // Delete a quest based on specific criteria
        // GET: Quest/DeleteQuestsByCriterion
        public IActionResult DeleteQuestsByCriterion()
        {
            return View();
        }

        // POST: Quest/DeleteQuestsByCriterion
        [HttpPost]
        public IActionResult DeleteQuestsByCriterion(string criterion)
        {
            if (string.IsNullOrEmpty(criterion))
            {
                ViewBag.ErrorMessage = "Criterion cannot be empty.";
                return View();
            }

            // Find quests that match the criterion
            var questsToDelete = _context.Quests
                .Where(q => q.Criteria.Contains(criterion))
                .ToList();

            if (!questsToDelete.Any())
            {
                ViewBag.ErrorMessage = "No quests found matching the specified criterion.";
                return View();
            }

            // Remove the quests from the database
            _context.Quests.RemoveRange(questsToDelete);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Quests matching the criterion were deleted successfully.";
            ViewBag.DeletedQuestCount = questsToDelete.Count;
            ViewBag.Criterion = criterion;  // Pass the entered criterion back to the view
            return View();
        }
    }
}
