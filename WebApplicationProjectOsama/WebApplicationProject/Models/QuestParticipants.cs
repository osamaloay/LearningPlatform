using System.Collections.Generic;
using WebApplicationProject.Models;

namespace WebApplicationProject.Models
{
    public class QuestParticipantsViewModel
    {
        public Quest Quest { get; set; }
        public List<Learner> Participants { get; set; }
    }
}
