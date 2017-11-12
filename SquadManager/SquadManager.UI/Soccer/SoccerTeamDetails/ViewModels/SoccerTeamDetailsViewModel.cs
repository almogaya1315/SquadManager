using SquadManager.UI.Base;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerTeamDetails.ViewModels
{
    public class SoccerTeamDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;

        private Team _team;

        public TeamViewModel TeamDetails { get; set; }
        public List<ManagerViewModel> Managers { get; set; }

        public SoccerTeamDetailsViewModel() { }
        public SoccerTeamDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _team = team;

            Collections = collections;

            Managers = Collections.ManagerViewModels;

            TeamDetails = new TeamViewModel(_changesManager)
            {
                Id = _team.Id,
                Name = _team.Name,
                Manager = Managers.Find(m => m.Id == _team.ManagerId),
                Captain = new SoccerPlayerViewModel(_team.Squad.Find(p => p.IsCaptain)),
                Nation = Collections.NationViewModels.Find(n => n.Id == _team.NationId),
                City = Collections.CityViewModels.Find(c => c.Id == _team.CityId),
                Sport = Collections.SportViewModels.Find(s => s.Id == _team.SportId),
            };
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
