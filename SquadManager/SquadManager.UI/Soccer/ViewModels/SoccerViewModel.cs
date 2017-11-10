using SquadManager.UI.Base;
using SquadManager.UI.Models;
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
        public readonly IChangeManager _changesManager;

        public SoccerViewModelSource Source { get; set; }

        public SoccerViewModel() { }
        public SoccerViewModel(TeamViewModel team, IChangeManager changeManager)
        {
            _changesManager = changeManager;

            Source = new SoccerViewModelSource(_changesManager);

            _changesManager.Changeables = new List<IChangeable>()
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
