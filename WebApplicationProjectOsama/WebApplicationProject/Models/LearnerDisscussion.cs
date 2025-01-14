using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class LearnerDisscussion
{
    public int ForumId { get; set; }

    public int Sid { get; set; }

    public string Post { get; set; } = null!;

    public DateTime TimePosted { get; set; }

    public virtual DisscussionForum Forum { get; set; } = null!;

    public virtual User SidNavigation { get; set; } = null!;
}
