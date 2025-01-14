namespace WebApplicationProject.Models
{
    public class LearnerProfileTempModel
    {
        
        public string Fname { get; set; }
        public string Lname { get; set; }

        public DateOnly? BirthDate { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        // Add property for file upload
        public IFormFile ProfileImage { get; set; }

        // Optionally, a URL to display the uploaded image
        public string ProfileImageUrl { get; set; }
    }
}
