using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebApplicationProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Configuration;
using System.Text.RegularExpressions;


public class AccountController : Controller
{
    private readonly WebsiteContext _context;
    private readonly  string _connectionString;

    public AccountController(WebsiteContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User model)
    {
        // Validation flags
        bool isValid = true;

        // Check if required fields are empty
        if (string.IsNullOrEmpty(model.Username))
        {
            TempData["UsernameError"] = "Username is required.";
            isValid = false;
        }

        if (string.IsNullOrEmpty(model.Password))
        {
            TempData["PasswordError"] = "Password is required.";
            isValid = false;
        }

        if (string.IsNullOrEmpty(model.Email))
        {
            TempData["EmailError"] = "Email is required.";
            isValid = false;
        }
        else
        {
            // Validate email format
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(model.Email, emailPattern))
            {
                TempData["EmailError"] = "Invalid email format.";
                isValid = false;
            }
        }

        if (string.IsNullOrEmpty(model.Role))
        {
            TempData["RoleError"] = "Role is required.";
            isValid = false;
        }

        if (!isValid)
        {
            return RedirectToAction("Register");
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Check if Username or Email already exists
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username = @Username OR Email = @Email", connection))
                {
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        TempData["DuplicateError"] = "Username or Email already exists.";
                        return RedirectToAction("Register");
                    }
                }

                // Insert user into database
                using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Role", model.Role);

                    if (model.Role == "Learner")
                    {
                        cmd.Parameters.AddWithValue("@FirstName", model.Learner?.Fname ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", model.Learner?.Lname ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", model.Learner?.Gender ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BirthDate", model.Learner?.BirthDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", model.Learner?.Country ?? (object)DBNull.Value);
                    }
                    else if (model.Role == "Instructor")
                    {
                        cmd.Parameters.AddWithValue("@Name", model.Instructor?.Name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Expertise", model.Instructor?.ExpertiseArea ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Qualification", model.Instructor?.LatestQualification ?? (object)DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToAction("Index", "Login");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            return RedirectToAction("Register");
        }
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Username");
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Clear(); // Clear all session data

        // Clear authentication cookies if you're using cookie-based authentication
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


        return RedirectToAction("Index", "Login"); // Redirect to login page
    }
}





