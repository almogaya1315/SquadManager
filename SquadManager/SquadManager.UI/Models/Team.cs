using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public int ManagerId { get; set; }
        public int NationId { get; set; }
        public int CityId { get; set; }
        public int SportId { get; set; }
        public string CrestImagePath { get; set; }

        public DateTime LastSave { get; set; }
        public List<SoccerPlayer> Squad { get; set; }
        public List<Formation> Formations { get; set; }

        public Team()
        {
            Squad = new List<SoccerPlayer>();
            Formations = new List<Formation>();
        }
    }
}
