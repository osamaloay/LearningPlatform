﻿using System;
using System.Collections.Generic;

namespace WebApplicationProject.Models;

public partial class Admin
{
    public int Aid { get; set; }

    public virtual User AidNavigation { get; set; } = null!;
}
