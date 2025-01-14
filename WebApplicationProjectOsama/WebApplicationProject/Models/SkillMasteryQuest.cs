using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class SkillMasteryQuest
{
    public int QuestId { get; set; }

    public string Skill { get; set; } = null!;

    public virtual Quest Quest { get; set; } = null!;
}
