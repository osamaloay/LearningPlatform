using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly WebsiteContext _context;
        private readonly string _connectionString;

        public LoginController(WebsiteContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Displays the login page
        }

        public async Task<IActionResult> Index(User user)
        {
            // Clear any previous session data before processing the login
            HttpContext.Session.Clear();

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                TempData["ErrorMessage"] = "Username and password are required.";
                if (string.IsNullOrEmpty(user.Username))
                {
                    TempData["UsernameError"] = "Username is required.";  // Specific error for username
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    TempData["PasswordError"] = "Password is required.";  // Specific error for password
                }
                return View();
            }

            try
            {
                var usernameParam = new SqlParameter("@Username", user.Username);
                var passwordParam = new SqlParameter("@Password", user.Password);

                var statusParam = new SqlParameter
                {
                    ParameterName = "@status",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output
                };

                var typeParam = new SqlParameter
                {
                    ParameterName = "@Type",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                // Execute the stored procedure
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UserLogin @Username, @Password, @status OUTPUT, @Type OUTPUT",
                    usernameParam, passwordParam, statusParam, typeParam
                );

                var loginStatus = (bool)statusParam.Value;
                var userType = typeParam.Value as string;

                if (loginStatus)
                {
                    // After successful login, fetch the user and set session variables
                    var loggedInUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
                    // Clear previous session data
                    HttpContext.Session.Clear();
                    Console.WriteLine("Session cleared");

                    // Set new session data after successful login
                    HttpContext.Session.SetString("Username", loggedInUser.Username);
                    HttpContext.Session.SetInt32("UserId", loggedInUser.Id);
                    Console.WriteLine($"New session data set: Username={loggedInUser.Username}, UserId={loggedInUser.Id}");

                    if (loggedInUser != null)
                    {
                        // Clear session and set the new user's data
                        HttpContext.Session.SetString("Username", loggedInUser.Username);
                        HttpContext.Session.SetInt32("UserId", loggedInUser.Id);

                        switch (userType?.ToLower())
                        {
                            case "learner":
                                return RedirectToAction("LearnerHome", "Home");
                            case "instructor":
                                return RedirectToAction("InstructorHome", "Home");
                            case "admin":
                                return RedirectToAction("AdminHome", "Home");
                            default:
                                TempData["ErrorMessage"] = "Unknown user type.";
                                break;
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "User not found.";
                    }
                }
                else
                {
                    // Username exists but password is incorrect
                    var userExists = await _context.Users.AnyAsync(u => u.Username == user.Username);

                    if (userExists)
                    {
                        TempData["PasswordError"] = "Incorrect password.";  // Specific password error
                    }
                    else
                    {
                        TempData["UsernameError"] = "Username not found.";  // Specific username error
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                Console.WriteLine($"Error during login: {ex.Message}");
            }


            return View(user);
        }




    }
}
