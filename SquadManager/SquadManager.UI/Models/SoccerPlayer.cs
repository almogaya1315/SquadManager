using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class SoccerPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsCaptain { get; set; }
        public Position Position { get; set; }
        public bool IsLineup { get; set; }
    }
}
