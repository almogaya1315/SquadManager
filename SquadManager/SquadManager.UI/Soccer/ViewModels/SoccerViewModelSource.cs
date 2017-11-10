using SquadManager.UI.Base;
using SquadManager.UI.Soccer.SoccerLineupDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerNavigationBar.ViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerSquadDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerTeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.ViewModels
{
    public class SoccerViewModelSource
    {
        // TODO: Injector

        public SoccerTeamDetailsViewModel SoccerTeamDetails { get; set; }
        public SoccerNavigationBarViewModel SoccerNavigationBar { get; set; }
        public SoccerSquadDetailsViewModel SoccerSquadDetails { get; set; }
        public SoccerPlayerDetailsViewModel SoccerPlayerDetails { get; set; }
        public SoccerLineupDetailsViewModel SoccerLineupDetails { get; set; }

        public SoccerViewModelSource(IChangeManager changeManager)
        {
            SoccerTeamDetails = new SoccerTeamDetailsViewModel(changeManager);
            SoccerNavigationBar = new SoccerNavigationBarViewModel(changeManager);
            SoccerSquadDetails = new SoccerSquadDetailsViewModel(changeManager);
            SoccerPlayerDetails = new SoccerPlayerDetailsViewModel(changeManager);
            SoccerLineupDetails = new SoccerLineupDetailsViewModel(changeManager);
        }
    }
}
