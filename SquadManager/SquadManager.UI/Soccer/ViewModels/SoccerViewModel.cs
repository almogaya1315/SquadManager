using SquadManager.UI.AppContainer.ViewModels;
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
    public class SoccerViewModel : ViewModel, IChangeable
    {
        public SoccerViewModelSource Source { get; set; }

        public SoccerViewModel() { }
        public SoccerViewModel(TeamViewModel team, Application app, CollectionFactory collections, ISquadRepository squadRepository, ViewModelBrowser browser, IChangeManager changeManager, Injector injector)
        {
            App = app;
            SquadRepository = squadRepository;
            Collections = collections;
            Browser = browser;

            Source = new SoccerViewModelSource(team, injector, changeManager);

            changeManager.Changeables = new List<IChangeable>()
            {
                this, 
                Source.SoccerNavigationBar,
                Source.SoccerTeamDetails,
                Source.SoccerSquadDetails,
                Source.SoccerPlayerDetails,
                Source.SoccerLineupDetails,
            };
        }
        
        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
