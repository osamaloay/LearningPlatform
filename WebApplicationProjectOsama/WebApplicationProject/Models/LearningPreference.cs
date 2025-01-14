﻿using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class LearningPreference
{
    public int Sid { get; set; }

    public string Preference { get; set; } = null!;

    public virtual Learner SidNavigation { get; set; } = null!;
}
