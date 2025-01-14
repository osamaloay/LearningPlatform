using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string Name { get; set; } = null!;

    public string LatestQualification { get; set; } = null!;

    public string ExpertiseArea { get; set; } = null!;

    public virtual ICollection<DisscussionForum> DisscussionForums { get; set; } = new List<DisscussionForum>();

    public virtual ICollection<EmotionalFeedbackReview> EmotionalFeedbackReviews { get; set; } = new List<EmotionalFeedbackReview>();

    public virtual ICollection<InstructorDisscussion> InstructorDisscussions { get; set; } = new List<InstructorDisscussion>();

    public virtual User InstructorNavigation { get; set; } = null!;

    public virtual ICollection<PathReview> PathReviews { get; set; } = new List<PathReview>();

    public virtual ICollection<Course> Cids { get; set; } = new List<Course>();
}
