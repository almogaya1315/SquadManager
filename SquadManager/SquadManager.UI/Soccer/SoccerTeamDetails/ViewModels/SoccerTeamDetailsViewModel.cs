using SquadManager.UI.Base;
using SquadManager.UI.ManagerDetails.ViewModels;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
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
        public int PlayerCount { get; set; }
        public int ReservesCount { get; set; }
        public int GoalKeeperCount { get; set; }
        public int DefendersCount { get; set; }
        public int MidfieldersCount { get; set; }
        public int AttackersCount { get; set; }

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
                Captain = _team.Squad.Exists(p => p.IsCaptain) ? new SoccerPlayerViewModel(_team.Squad.Find(p => p.IsCaptain)) : new SoccerPlayerViewModel(),
                Nation = Collections.NationViewModels.Find(n => n.Id == _team.NationId),
                City = Collections.CityViewModels.Find(c => c.Id == _team.CityId),
                Sport = Collections.SportViewModels.Find(s => s.Id == _team.SportId),
                Crest = new EditableCellViewModel(_team.CrestImagePath),
                Squad = _team.Squad.Count > 0 ? _team.Squad.Select(p => new SoccerPlayerViewModel(p)).ToList() : new List<SoccerPlayerViewModel>(),
            };

            PlayerCount = TeamDetails.Squad.Count;
            ReservesCount = TeamDetails.Squad.Count(p => !p.IsLineup);
            GoalKeeperCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.GoalKeepers);
            DefendersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Defenders);
            MidfieldersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Midfielders);
            AttackersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Attackers);
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
