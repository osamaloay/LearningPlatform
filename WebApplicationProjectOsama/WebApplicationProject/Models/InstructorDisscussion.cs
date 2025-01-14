using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class InstructorDisscussion
{
    public int ForumId { get; set; }

    public int InstructorId { get; set; }

    public string Post { get; set; } = null!;

    public DateTime TimePosted { get; set; }

    public virtual DisscussionForum Forum { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
