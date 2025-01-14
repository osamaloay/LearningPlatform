using System;
using System.Collections.Generic;
using WebApplicationProject.Models;

namespace WebApplicationProject.Models
{
    public class QuestActionsViewModel
    {
        public List<CollaborativeQuest> CollaborativeQuests { get; set; }
        public List<LearnersCollaboration> ActiveQuests { get; set; }
        public List<Acheivement> Achievements { get; set; }
    }
}
