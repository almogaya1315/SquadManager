using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Extensions;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class SoccerFieldDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changeManager;
        private Team _teamModel;

        private TeamViewModel _team;
        public TeamViewModel Team
        {
            get { return _team; }
            set
            {
                _team = value;
                RaisePropertyChanged();
            }
        }

        public FormationViewModel SelectedFormation { get; set; }

        public SoccerPlayerViewModel _firstSubstitute;
        public SoccerPlayerViewModel _secondSubstitute;

        public ICommand Substitute { get; set; }

        public SoccerFieldDetailsViewModel(Team team, IChangeManager changeManager, CollectionFactory collections, ISquadRepository squadRepository)
        {
            _changeManager = changeManager;
            _teamModel = team;

            SquadRepository = squadRepository;
            Collections = collections;

            Team = new TeamViewModel(team, _changeManager, Collections);
            Team.Formations = _teamModel.Formations.Select(f => SetFormation(f, f.Name.Contains("Default"))).ToList();

            // TODO: Find(f => f.IsInUse)
            SelectedFormation = Team.Formations.First();  
            SelectedFormation.Lineup.RemoveFirstNames();

            Substitute = new RelayCommand(SubstitutePlayers, CanSubstitute);
        }

        private FormationViewModel SetFormation(Formation formation, bool isDefault = true)
        {
            return new FormationViewModel()
            {
                Id = formation.Id,
                Name = formation.Name,
                Player_1 = SetFormationPlayer(formation.Player_1Id.Value, formation.Player_1X, formation.Player_1Y, isDefault, 1),
                Player_2 = SetFormationPlayer(formation.Player_2Id.Value, formation.Player_2X, formation.Player_2Y, isDefault, 2),
                Player_3 = SetFormationPlayer(formation.Player_3Id.Value, formation.Player_3X, formation.Player_3Y, isDefault, 3),
                Player_4 = SetFormationPlayer(formation.Player_4Id.Value, formation.Player_4X, formation.Player_4Y, isDefault, 4),
                Player_5 = SetFormationPlayer(formation.Player_5Id.Value, formation.Player_5X, formation.Player_5Y, isDefault, 5),
                Player_6 = SetFormationPlayer(formation.Player_6Id.Value, formation.Player_6X, formation.Player_6Y, isDefault, 6),
                Player_7 = SetFormationPlayer(formation.Player_7Id.Value, formation.Player_7X, formation.Player_7Y, isDefault, 7),
                Player_8 = SetFormationPlayer(formation.Player_8Id.Value, formation.Player_8X, formation.Player_8Y, isDefault, 8),
                Player_9 = SetFormationPlayer(formation.Player_9Id.Value, formation.Player_9X, formation.Player_9Y, isDefault, 9),
                Player_10 = SetFormationPlayer(formation.Player_10Id.Value, formation.Player_10X, formation.Player_10Y, isDefault, 10),
                Player_11 = SetFormationPlayer(formation.Player_11Id.Value, formation.Player_11X, formation.Player_11Y, isDefault, 11),
            };
        }

        private SoccerPlayerViewModel SetFormationPlayer(int? playerId, int playerX, int playerY, bool isDefaultFormation, int? playerOrder = null)
        {
            SoccerPlayerViewModel player;
            if (!isDefaultFormation)
            {
                if (!playerId.HasValue) return null;
                player = Team.Squad.Find(p => p.Id == playerId.Value);
                player.X = playerX;
                player.Y = playerY;
                return player;
            }
            else if (playerOrder.HasValue)
            {
                PositionRole? role = null;
                switch (playerOrder)
                {
                    case 1:
                        role = PositionRole.GK;
                        break;
                    case 2:
                        role = PositionRole.LB;
                        break;
                    case 3:
                        role = PositionRole.CB;
                        break;
                    case 4:
                        role = PositionRole.CB;
                        break;
                    case 5:
                        role = PositionRole.RB;
                        break;
                    case 6:
                        role = PositionRole.LW;
                        break;
                    case 7:
                        role = PositionRole.CM;
                        break;
                    case 8:
                        role = PositionRole.CM;
                        break;
                    case 9:
                        role = PositionRole.RW;
                        break;
                    case 10:
                        role = PositionRole.CAM;
                        break;
                    case 11:
                        role = PositionRole.ST;
                        break;
                }
                if (role.HasValue)
                {
                    player = Team.Squad.FirstOrDefault(p => (PositionRole)p.Position.Value == role.Value);
                    player.X = playerX;
                    player.Y = playerY;
                    return player;
                }
                else throw new InvalidOperationException("playerOrder value in not between 1 and 11.");
            }
            else throw new InvalidOperationException("playerOrder parameter cannot be null when setting the default formation.");
        }

        private bool CanSubstitute()
        {
            return _firstSubstitute != null && _secondSubstitute != null;
        }

        private void SubstitutePlayers()
        {
            // TODO: 

            var firstSubX = _firstSubstitute.X;
            var firstSubY = _firstSubstitute.Y;

            // VIEW MODEL
            // sub inside 'field details'
            if (SelectedFormation.Lineup.Contains(_firstSubstitute) && SelectedFormation.Lineup.Contains(_secondSubstitute))
            {
                _firstSubstitute.X = _secondSubstitute.X;
                _firstSubstitute.Y = _secondSubstitute.Y;
                _secondSubstitute.X = firstSubX;
                _secondSubstitute.Y = firstSubY;
            }
            // sub first sub from 'field details' to second sub in 'lineup details'
            else if (SelectedFormation.Lineup.Contains(_firstSubstitute) && !SelectedFormation.Lineup.Contains(_secondSubstitute))
            {
                _firstSubstitute.Name.SetValueToBinding(_secondSubstitute.Name.Value);
                _firstSubstitute.Position.SetValueToBinding(_secondSubstitute.Position.Value);
                _firstSubstitute.X = firstSubX;
                _firstSubstitute.Y = firstSubY;

                SelectedFormation.Lineup.RemoveFirstNames();
            }
            // sub first sub from 'lineup details' to second sub in 'field details'
            else if (!SelectedFormation.Lineup.Contains(_firstSubstitute) && SelectedFormation.Lineup.Contains(_secondSubstitute))
            {

            }
            // sub inside 'lineup details'
            else if (!SelectedFormation.Lineup.Contains(_firstSubstitute) && !SelectedFormation.Lineup.Contains(_secondSubstitute))
            {

            }

            // MODEL
            var formation = _teamModel.Formations.Find(f => f.Id == SelectedFormation.Id);
            if (formation.Player_1Id == _firstSubstitute.Id)
            {

            }

            // DB
            // CHANGE

            // RESET SUB VM => should be in VIEW MODEL update??
            _firstSubstitute.SetIsSelectedBinding(false);
            _secondSubstitute.SetIsSelectedBinding(false);
            _firstSubstitute = null;
            _secondSubstitute = null;
        }

        public void Changed(ChangeArgs args)
        {
            if (args is SubstitutionArgs)
            {
                var subArgs = (SubstitutionArgs)args;
                var firstSub = subArgs.FirstSub;
                var secondSub = subArgs.SecondSub;
                if (args.Type == ChangeType.SubSelected)
                {
                    if (_firstSubstitute == null)
                    {
                        _firstSubstitute = SelectedFormation.Lineup.FirstOrDefault(p => p.Id == firstSub.Id);// ?? Team.Squad.Find(p => p.Id == firstSub.Id);
                    }
                    else if (firstSub.Id == _firstSubstitute.Id && secondSub == null) return;

                    if (secondSub != null)
                    {
                        if (_secondSubstitute == null)
                        {
                            _secondSubstitute = SelectedFormation.Lineup.FirstOrDefault(p => p.Id == secondSub.Id) ?? Team.Squad.Find(p => p.Id == secondSub.Id); 
                        }
                        else if (secondSub.Id == _secondSubstitute.Id) return;
                    }
                }
                else if (args.Type == ChangeType.SubDeselect)
                {
                    if (firstSub != null)
                    {
                        _firstSubstitute = null;
                    }
                    else if (secondSub != null)
                    {
                        _secondSubstitute = null;
                    }
                }
            }
        }
    }
}
