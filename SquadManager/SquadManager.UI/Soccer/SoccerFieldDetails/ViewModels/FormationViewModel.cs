using SquadManager.UI.Base;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class FormationViewModel : ViewModel
    {
        public string Name { get; set; }

        public SoccerPlayerViewModel Player_1 { get; set; }
        public SoccerPlayerViewModel Player_2 { get; set; }
        public SoccerPlayerViewModel Player_3 { get; set; }
        public SoccerPlayerViewModel Player_4 { get; set; }
        public SoccerPlayerViewModel Player_5 { get; set; }
        public SoccerPlayerViewModel Player_6 { get; set; }
        public SoccerPlayerViewModel Player_7 { get; set; }
        public SoccerPlayerViewModel Player_8 { get; set; }
        public SoccerPlayerViewModel Player_9 { get; set; }
        public SoccerPlayerViewModel Player_10 { get; set; }
        public SoccerPlayerViewModel Player_11 { get; set; }
    }
}
