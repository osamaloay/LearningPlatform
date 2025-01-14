using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class DisscussionForum
{
    public int ForumId { get; set; }

    public int ModuleId { get; set; }

    public int Cid { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? LastActivity { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Description { get; set; }

    public int? InstructorId { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual ICollection<InstructorDisscussion> InstructorDisscussions { get; set; } = new List<InstructorDisscussion>();

    public virtual ICollection<LearnerDisscussion> LearnerDisscussions { get; set; } = new List<LearnerDisscussion>();

    public virtual Module Module { get; set; } = null!;
}
