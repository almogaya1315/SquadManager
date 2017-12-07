using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Enums;
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

        private SoccerPlayerViewModel _firstSubstitute;
        private SoccerPlayerViewModel _secondSubstitute;

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

                Lineup = new SquadList<SoccerPlayerViewModel>()
                {
                    SetFormationPlayer(formation.Player_1Id.Value, formation.Player_1X, formation.Player_1Y, isDefault, 1),
                    SetFormationPlayer(formation.Player_2Id.Value, formation.Player_2X, formation.Player_2Y, isDefault, 2),
                    SetFormationPlayer(formation.Player_3Id.Value, formation.Player_3X, formation.Player_3Y, isDefault, 3),
                    SetFormationPlayer(formation.Player_4Id.Value, formation.Player_4X, formation.Player_4Y, isDefault, 4),
                    SetFormationPlayer(formation.Player_5Id.Value, formation.Player_5X, formation.Player_5Y, isDefault, 5),
                    SetFormationPlayer(formation.Player_6Id.Value, formation.Player_6X, formation.Player_6Y, isDefault, 6),
                    SetFormationPlayer(formation.Player_7Id.Value, formation.Player_7X, formation.Player_7Y, isDefault, 7),
                    SetFormationPlayer(formation.Player_8Id.Value, formation.Player_8X, formation.Player_8Y, isDefault, 8),
                    SetFormationPlayer(formation.Player_9Id.Value, formation.Player_9X, formation.Player_9Y, isDefault, 9),
                    SetFormationPlayer(formation.Player_10Id.Value, formation.Player_10X, formation.Player_10Y, isDefault, 10),
                    SetFormationPlayer(formation.Player_11Id.Value, formation.Player_11X, formation.Player_11Y, isDefault, 11),
                },
            };
        }

        private SoccerPlayerViewModel SetFormationPlayer(int? playerId, int playerX, int playerY, bool isDefaultFormation, int? playerOrder = null)
        {
            SoccerPlayerViewModel player;
            if (!isDefaultFormation)
            {
                if (!playerId.HasValue) return null;
                player = Team.Squad.Find(p => p.Id == playerId.Value);

                var playerModel = _teamModel.Squad.Find(p => p.Id == player.Id);
                if (playerModel.Rotation != RotationTeam.Lineup)
                {
                    playerModel.Rotation = RotationTeam.Lineup;
                    SquadRepository.UpdatePlayer(playerModel);
                }

                player.RotationTeam = new CellViewModel(RotationTeam.Lineup);
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
            var firstSubX = _firstSubstitute.X;
            var firstSubY = _firstSubstitute.Y;
            var secondSubX = _secondSubstitute.X;
            var secondSubY = _secondSubstitute.Y;

            SubFrom? isInsideLineupSection = null;

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
                SubstituteBetweenChangeables(SubFrom.FieldDetailsSection, firstSubX, firstSubY);
                SelectedFormation.Lineup.RemoveFirstNames();
            }
            // sub first sub from 'lineup details' to second sub in 'field details'
            else if (!SelectedFormation.Lineup.Contains(_firstSubstitute) && SelectedFormation.Lineup.Contains(_secondSubstitute))
            {
                SubstituteBetweenChangeables(SubFrom.LineupDetailsSection, secondSubX, secondSubY);
                SelectedFormation.Lineup.RemoveFirstNames();
            }
            // sub inside 'lineup details'
            else if (!SelectedFormation.Lineup.Contains(_firstSubstitute) && !SelectedFormation.Lineup.Contains(_secondSubstitute))
            {
                isInsideLineupSection = SubFrom.InsideLineupDetailsSection;
            }

            // MODEL
            SoccerPlayer firstSubModel = _teamModel.Squad.FirstOrDefault(p => p.Id == _firstSubstitute.Id);
            SoccerPlayer secondSubModel = _teamModel.Squad.FirstOrDefault(p => p.Id == _secondSubstitute.Id);
            var originalFirstSub = firstSubModel.RefferenceCopy();
            var originalSecondSub = secondSubModel.RefferenceCopy();

            if (isInsideLineupSection.HasValue && isInsideLineupSection.Value == SubFrom.InsideLineupDetailsSection)
            {
                firstSubModel.Rotation = (RotationTeam)_secondSubstitute.RotationTeam.Value;
                secondSubModel.Rotation = (RotationTeam)_firstSubstitute.RotationTeam.Value;
            }
            else
            {
                firstSubModel.Rotation = (RotationTeam)_firstSubstitute.RotationTeam.Value;
                secondSubModel.Rotation = (RotationTeam)_secondSubstitute.RotationTeam.Value;
            }

            var formation = _teamModel.Formations.Find(f => f.Id == SelectedFormation.Id);
            var originalFormation = formation.RefferenceCopy();
            if (originalFormation.LineupIds.Contains(_firstSubstitute.Id))
                UpdateFormationModel(formation, _firstSubstitute.Id);
            if (originalFormation.LineupIds.Contains(_secondSubstitute.Id))
                UpdateFormationModel(formation, _secondSubstitute.Id);

            // DB
            SquadRepository.UpdatePlayer(firstSubModel);
            SquadRepository.UpdatePlayer(secondSubModel);
            if (!isInsideLineupSection.HasValue) SquadRepository.UpdateFormation(formation);

            // CHANGE
            if (firstSubModel == null || secondSubModel == null) throw new InvalidOperationException("Models were not found from the view model id's");
            var subArgs = new SubstitutionArgs(ChangeType.SubConfirmed, originalFirstSub, originalSecondSub)
            {
                firstSubX = _firstSubstitute.X,
                firstSubY = _firstSubstitute.Y,
                secondSubX = _secondSubstitute.X,
                secondSubY = _secondSubstitute.Y,
            };
            _changeManager.Change(subArgs);

            // RESET SUB VM => should be in VIEW MODEL update??
            _firstSubstitute.SetIsSelectedBinding(false);
            _secondSubstitute.SetIsSelectedBinding(false);
            _firstSubstitute = null;
            _secondSubstitute = null;
        }

        private void SubstituteBetweenChangeables(SubFrom from, int subX, int subY)
        {
            var lineup = SelectedFormation.Lineup;
            var firstSubRotationValue = (RotationTeam)_firstSubstitute.RotationTeam.Value;
            var secondSubRotationValue = (RotationTeam)_secondSubstitute.RotationTeam.Value;

            switch (from)
            {
                case SubFrom.LineupDetailsSection:
                    var secondSubIndex = lineup.IndexOf(_secondSubstitute);
                    lineup.Remove(_secondSubstitute);

                    _secondSubstitute.X = 0;
                    _secondSubstitute.Y = 0;
                    _secondSubstitute.RotationTeam = new CellViewModel(firstSubRotationValue);

                    _firstSubstitute.X = subX;
                    _firstSubstitute.Y = subY;
                    _firstSubstitute.RotationTeam = new CellViewModel(secondSubRotationValue);

                    lineup.Insert(secondSubIndex, _firstSubstitute);
                    break;
                case SubFrom.FieldDetailsSection:
                    var firstSubIndex = lineup.IndexOf(_firstSubstitute);
                    lineup.Remove(_firstSubstitute);

                    _firstSubstitute.X = 0;
                    _firstSubstitute.Y = 0;
                    _firstSubstitute.RotationTeam = new CellViewModel(secondSubRotationValue);

                    _secondSubstitute.X = subX;
                    _secondSubstitute.Y = subY;
                    _secondSubstitute.RotationTeam = new CellViewModel(firstSubRotationValue);

                    lineup.Insert(firstSubIndex, _secondSubstitute);
                    break;
            }

            SelectedFormation.Lineup = new SquadList<SoccerPlayerViewModel>(lineup);
        }

        private void UpdateFormationModel(Formation formation, int subId)
        {
            foreach (var prop in formation.GetType().GetProperties())
            {
                if (prop.Name.Contains("Player_") && prop.Name.Contains("Id"))
                {
                    var IdPropValue = (prop.GetValue(formation) as int?).Value;
                    if (IdPropValue == subId)
                    {
                        var propId = prop;
                        var propIdValue = propId.GetValue(formation);
                        propId.SetValue(formation, subId == _firstSubstitute.Id ? _secondSubstitute.Id : _firstSubstitute.Id);
                        propIdValue = propId.GetValue(formation);

                        //var playerNumber = string.Empty;
                        //foreach (var c in prop.Name) if (Char.IsNumber(c)) playerNumber += c;

                        //var XPropName = $"Player_{playerNumber}X";
                        //var XProp = formation.GetType().GetProperties().First(p => p.Name == XPropName);
                        //var XPropValue = XProp.GetValue(formation);
                        //XProp.SetValue(formation, subId == _secondSubstitute.Id ? _secondSubstitute.X : _firstSubstitute.X);
                        //XPropValue = XProp.GetValue(formation);

                        //var YPropName = $"Player_{playerNumber}Y";
                        //var YProp = formation.GetType().GetProperties().First(p => p.Name == YPropName);
                        //var YPropValue = YProp.GetValue(formation);
                        //YProp.SetValue(formation, subId == _secondSubstitute.Id ? _secondSubstitute.Y : _firstSubstitute.Y);
                        //YPropValue = YProp.GetValue(formation);

                        return;
                    }
                }
            }
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
                        _firstSubstitute = SelectedFormation.Lineup.FirstOrDefault(p => p.Id == firstSub.Id) ?? Team.Squad.Find(p => p.Id == firstSub.Id);
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
