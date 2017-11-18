using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.Soccer.SoccerLineupDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerNavigationBar.ViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerSquadDetails.ViewModels;
using SquadManager.UI.Soccer.SoccerTeamDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
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

        public SoccerViewModelSource(Team team, Injector injector, IChangeManager changeManager, CollectionFactory collections, ISquadRepository squadRepository)
        {
            SoccerTeamDetails = injector.New<SoccerTeamDetailsViewModel>(team: team); 
            SoccerNavigationBar = new SoccerNavigationBarViewModel(team, changeManager);
            SoccerSquadDetails = new SoccerSquadDetailsViewModel(team, changeManager, collections, squadRepository);
            SoccerPlayerDetails = new SoccerPlayerDetailsViewModel(team, changeManager);
            SoccerLineupDetails = new SoccerLineupDetailsViewModel(team, changeManager);
        }
    }
}
