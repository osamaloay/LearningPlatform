using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual Learner? Learner { get; set; }

    public virtual ICollection<LearnerDisscussion> LearnerDisscussions { get; set; } = new List<LearnerDisscussion>();
}
