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

        private int _playerCount;
        public int PlayerCount
        {
            get { return _playerCount; }
            set
            {
                if (Equals(_playerCount, value)) return;

                _playerCount = value;
                RaisePropertyChanged();
            }
        }
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

            SetTeamSquadDetails();
        }

        private void SetTeamSquadDetails()
        {
            PlayerCount = TeamDetails.Squad.Count;
            ReservesCount = TeamDetails.Squad.Count(p => !p.IsLineup);
            GoalKeeperCount = TeamDetails.Squad.Count(p => GetPositionGroup((PositionRole)p.Position.Value) == PositionGroup.GoalKeepers);
            DefendersCount = TeamDetails.Squad.Count(p => GetPositionGroup((PositionRole)p.Position.Value) == PositionGroup.Defenders);
            MidfieldersCount = TeamDetails.Squad.Count(p => GetPositionGroup((PositionRole)p.Position.Value) == PositionGroup.Midfielders);
            AttackersCount = TeamDetails.Squad.Count(p => GetPositionGroup((PositionRole)p.Position.Value) == PositionGroup.Attackers);
            InjuredCount = TeamDetails.Squad.Count(p => p.IsInjured);
            OnLoanCount = TeamDetails.Squad.Count(p => p.IsOnLoan);
            LoanedCount = TeamDetails.Squad.Count(p => p.IsLoaned);
        }

        private PositionGroup GetPositionGroup(PositionRole role)
        {
            switch (role)
            {
                case PositionRole.GK: return PositionGroup.GoalKeepers;
                case PositionRole.RB:
                case PositionRole.RWB:
                case PositionRole.CB:
                case PositionRole.LWB:
                case PositionRole.LB: return PositionGroup.Defenders;
                case PositionRole.RDM:
                case PositionRole.CDM:
                case PositionRole.LDM:
                case PositionRole.LM:
                case PositionRole.LWM:
                case PositionRole.CM:
                case PositionRole.RWM:
                case PositionRole.RM:
                case PositionRole.RCAM:
                case PositionRole.CAM:
                case PositionRole.LCAM: return PositionGroup.Midfielders;
                case PositionRole.RW:
                case PositionRole.LW:
                case PositionRole.RF:
                case PositionRole.CF:
                case PositionRole.LF:
                case PositionRole.RS:
                case PositionRole.ST:
                case PositionRole.LS: return PositionGroup.Attackers;
            }

            throw new InvalidOperationException();
        }

        public void Changed(ChangeArgs args)
        {
            if (args.Type != ChangeType.PlayerAdded) return;

            switch (args.Type)
            {
                case ChangeType.TeamChanged:
                    break;
                case ChangeType.PlayerAdded:
                    var playerArgs = (SoccerPlayerAddedArgs)args;
                    TeamDetails.Squad.Add(new SoccerPlayerViewModel(playerArgs.Player, Collections));
                    SetTeamSquadDetails();
                    break;
                default:
                    break;
            }
        }
    }
}
