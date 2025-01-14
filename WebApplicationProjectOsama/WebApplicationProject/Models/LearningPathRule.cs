﻿using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class LearningPathRule
{
    public string ApadtiveRule { get; set; } = null!;

    public int PathId { get; set; }

    public virtual LearningPath Path { get; set; } = null!;
}
