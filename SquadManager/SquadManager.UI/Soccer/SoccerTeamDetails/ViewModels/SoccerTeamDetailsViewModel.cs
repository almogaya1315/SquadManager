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

        private Team _teamModel;

        public TeamViewModel TeamDetails { get; set; }
        public List<ManagerViewModel> Managers { get; set; }
        public int PlayerCount { get; set; }
        public int ReservesCount { get; set; }
        public int GoalKeeperCount { get; set; }
        public int DefendersCount { get; set; }
        public int MidfieldersCount { get; set; }
        public int AttackersCount { get; set; }
        public int InjuredCount { get; set; }
        public int OnLoanCount { get; set; }
        public int LoanedCount { get; set; }

        public SoccerTeamDetailsViewModel() { }
        public SoccerTeamDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _teamModel = team;

            Collections = collections;

            Managers = Collections.ManagerViewModels;

            TeamDetails = new TeamViewModel(_teamModel, _changesManager, Collections);
            TeamDetails.Manager = Managers.Find(p => p.Id == TeamDetails.Manager.Id);

            PlayerCount = TeamDetails.Squad.Count;
            ReservesCount = TeamDetails.Squad.Count(p => !p.IsLineup);
            GoalKeeperCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.GoalKeepers);
            DefendersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Defenders);
            MidfieldersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Midfielders);
            AttackersCount = TeamDetails.Squad.Count(p => (p.Position.Value as Position).Group == PositionGroup.Attackers);
            InjuredCount = TeamDetails.Squad.Count(p => p.IsInjured);
            OnLoanCount = TeamDetails.Squad.Count(p => p.IsOnLoan);
            LoanedCount = TeamDetails.Squad.Count(p => p.IsLoaned);
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
