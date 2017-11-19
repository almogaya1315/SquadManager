using SquadManager.UI.Base;
using SquadManager.UI.Enums;
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
        private int _reservesCount;
        public int ReservesCount
        {
            get { return _reservesCount; }
            set
            {
                _reservesCount = value;
                RaisePropertyChanged();
            }
        }
        private int _goalKeeperCount;
        public int GoalKeeperCount
        {
            get { return _goalKeeperCount; }
            set
            {
                _goalKeeperCount = value;
                RaisePropertyChanged();
            }
        }
        private int _defendersCount;
        public int DefendersCount
        {
            get { return _defendersCount; }
            set
            {
                _defendersCount = value;
                RaisePropertyChanged();
            }
        }
        private int _midfieldersCount;
        public int MidfieldersCount
        {
            get { return _midfieldersCount; }
            set
            {
                _midfieldersCount = value;
                RaisePropertyChanged();
            }
        }
        private int _attackersCount;
        public int AttackersCount
        {
            get { return _attackersCount; }
            set
            {
                _attackersCount = value;
                RaisePropertyChanged();
            }
        }
        private int _injuredCount;
        public int InjuredCount
        {
            get { return _injuredCount; }
            set
            {
                _injuredCount = value;
                RaisePropertyChanged();
            }
        }
        private int _onLoanCount;
        public int OnLoanCount
        {
            get { return _onLoanCount; }
            set
            {
                _onLoanCount = value;
                RaisePropertyChanged();
            }
        }
        private int _loanedCount;
        public int LoanedCount
        {
            get { return _loanedCount; }
            set
            {
                _loanedCount = value;
                RaisePropertyChanged();
            }
        }

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
            if (args.Type != ChangeType.PlayerAdded && args.Type != ChangeType.PlayerChanged && args.Type != ChangeType.PlayerDeleted) return;

            switch (args.Type)
            {
                case ChangeType.PlayerAdded:
                    var addedPlayerArgs = (SoccerPlayerArgs)args;
                    TeamDetails.Squad.Add(new SoccerPlayerViewModel(addedPlayerArgs.PlayerValues, Collections, _changesManager));
                    SetTeamSquadDetails();
                    break;
                case ChangeType.PlayerChanged:
                    var changedPlayerArgs = (SoccerPlayerArgs)args;
                    var player = TeamDetails.Squad.Find(p => p.Id == changedPlayerArgs.PlayerValues.Id);

                    if (changedPlayerArgs.Column == ColumnName.IsCaptain && changedPlayerArgs.PlayerValues.IsCaptain)
                    {
                        if (TeamDetails.Squad.Exists(p => (bool)p.IsCaptain.Value))
                            TeamDetails.Squad.Find(p => (bool)p.IsCaptain.Value).IsCaptain.SetValueToBinding(false);
                        player.IsCaptain.SetValueToBinding(changedPlayerArgs.PlayerValues.IsCaptain);
                        TeamDetails.Captain = player;
                    }

                    if (changedPlayerArgs.Column == ColumnName.Position)
                    {
                        player.Position.SetValueToBinding(changedPlayerArgs.PlayerValues.Position.Role);
                        SetTeamSquadDetails();
                    }

                    break;
                case ChangeType.PlayerDeleted:
                    var deletedPlayerArgs = (SoccerPlayerArgs)args;
                    var deletedPlayer = TeamDetails.Squad.Find(p => p.Id == deletedPlayerArgs.PlayerValues.Id);
                    if ((bool)deletedPlayer.IsCaptain.Value) TeamDetails.Captain = null;
                    TeamDetails.Squad.Remove(deletedPlayer);
                    SetTeamSquadDetails();
                    break;
                default:
                    break;
            }
        }
    }
}
