﻿using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class LearnerMastery
{
    public int Sid { get; set; }

    public string? CompletionStatus { get; set; }

    public int QuestId { get; set; }

    public virtual Quest Quest { get; set; } = null!;

    public virtual Learner SidNavigation { get; set; } = null!;
}
