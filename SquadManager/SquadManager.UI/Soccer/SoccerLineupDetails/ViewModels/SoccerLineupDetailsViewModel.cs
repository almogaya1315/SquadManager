using SquadManager.UI.Base;
using SquadManager.UI.Enums;
using SquadManager.UI.Extensions;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerLineupDetails.ViewModels
{
    public class SoccerLineupDetailsViewModel : ViewModel, IChangeable
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

        private SquadList<SoccerPlayerViewModel> _substitutions;
        public SquadList<SoccerPlayerViewModel> Substitutions
        {
            get { return _substitutions; }
            set
            {
                _substitutions = value;
                RaisePropertyChanged();
            }
        }
        private SquadList<SoccerPlayerViewModel> _reserves;
        public SquadList<SoccerPlayerViewModel> Reserves
        {
            get { return _reserves; }
            set
            {
                _reserves = value;
                RaisePropertyChanged();
            }
        }

        public SoccerPlayerViewModel _firstSubstitute;
        public SoccerPlayerViewModel _secondSubstitute;

        public SoccerLineupDetailsViewModel(Team team, IChangeManager changeManager, CollectionFactory collections)
        {
            _changeManager = changeManager;
            _teamModel = team;

            Collections = collections;
            Team = new TeamViewModel(team, _changeManager, Collections);

            SoccerPlayerViewModel.IsSelectedPropertyChanged += SoccerPlayerViewModel_IsSelectedPropertyChanged;

            Substitutions = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Substitute).ToSquadList();
            if (Substitutions.Count < 7) Substitutions.Add(new SoccerPlayerViewModel() { Name = new EditableCellViewModel(string.Empty, _changeManager), Position = new ComboBoxCellViewModel(null, null, _changeManager) });
            Substitutions.RemoveFirstNames();
            Substitutions.ArrangePositionRoleAsec();

            Reserves = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Reserves).ToSquadList();
            Reserves.RemoveFirstNames();
            Reserves.ArrangePositionRoleAsec();
        }

        private void SoccerPlayerViewModel_IsSelectedPropertyChanged(object sender, IsSelectedEventArgs args)
        {
            var player = (SoccerPlayerViewModel)sender;

            if (args.IsSelected)
            {
                if (_firstSubstitute == null)
                {
                    _firstSubstitute = player;
                    var subArgs = new SubstitutionArgs(ChangeType.SubSelected, _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy())
                    {
                        firstSubX = _firstSubstitute.X,
                        firstSubY = _firstSubstitute.Y,
                        secondSubX = 0,
                        secondSubY = 0,
                    };
                    _changeManager.Change(subArgs);
                }
                else if (_firstSubstitute != null && _secondSubstitute == null)
                {
                    _secondSubstitute = player;
                    var subArgs = new SubstitutionArgs(ChangeType.SubSelected, _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy(), _teamModel.Squad.Find(p => p.Id == _secondSubstitute.Id).RefferenceCopy())
                    {
                        firstSubX = _firstSubstitute.X,
                        firstSubY = _firstSubstitute.Y,
                        secondSubX = _secondSubstitute.X,
                        secondSubY = _secondSubstitute.Y,
                    };
                    _changeManager.Change(subArgs);
                }
                else if (_firstSubstitute != null && _secondSubstitute != null) player.SetIsSelectedBinding(false);
            }
            else
            {
                if (_firstSubstitute != null && _firstSubstitute.Id == player.Id)
                {
                    _firstSubstitute = null;
                    var subArgs = new SubstitutionArgs(ChangeType.SubDeselect, null, _secondSubstitute != null ? _teamModel.Squad.Find(p => p.Id == _secondSubstitute.Id).RefferenceCopy() : null)
                    {
                        firstSubX = 0,
                        firstSubY = 0,
                        secondSubX = _secondSubstitute.X,
                        secondSubY = _secondSubstitute.Y,
                    };
                    _changeManager.Change(subArgs);
                }
                else if (_secondSubstitute != null && _secondSubstitute.Id == player.Id)
                {
                    _secondSubstitute = null;
                    var subArgs = new SubstitutionArgs(ChangeType.SubDeselect, _firstSubstitute != null ? _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy() : null, null)
                    {
                        firstSubX = _firstSubstitute.X,
                        firstSubY = _firstSubstitute.Y,
                        secondSubX = 0,
                        secondSubY = 0,
                    };
                    _changeManager.Change(subArgs);
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

                switch (args.Type)
                {
                    case ChangeType.SubSelected:
                        if (_firstSubstitute == null)
                        {
                            _firstSubstitute = Team.Squad.Find(p => p.Id == firstSub.Id);
                        }
                        else if (firstSub.Id == _firstSubstitute.Id && secondSub == null) return;

                        if (secondSub != null)
                        {
                            if (_secondSubstitute == null)
                            {
                                _secondSubstitute = Team.Squad.Find(p => p.Id == secondSub.Id);
                            }
                            else if (secondSub.Id == _secondSubstitute.Id) return;
                        }
                        break;
                    case ChangeType.SubDeselect:
                        if (firstSub != null)
                        {
                            _firstSubstitute = null;
                        }
                        else if (secondSub != null)
                        {
                            _secondSubstitute = null;
                        }
                        break;
                    case ChangeType.SubConfirmed:
                        _firstSubstitute.RotationTeam.Value = firstSub.Rotation;
                        _secondSubstitute.RotationTeam.Value = secondSub.Rotation; 

                        var firstSubIndex = Substitutions.Contains(_firstSubstitute) ? Substitutions.IndexOf(_firstSubstitute) : Reserves.IndexOf(_firstSubstitute);
                        var secondSubIndex = Substitutions.Contains(_secondSubstitute) ? Substitutions.IndexOf(_secondSubstitute) : Reserves.IndexOf(_secondSubstitute);

                        var firstSubX = subArgs.secondSubX;
                        var firstSubY = subArgs.secondSubY;
                        var secondSubX = subArgs.firstSubX;
                        var secondSubY = subArgs.firstSubY;

                        // sub inside 'lineup details'
                        if ((RotationTeam)_firstSubstitute.RotationTeam.Value != RotationTeam.Lineup && (RotationTeam)_secondSubstitute.RotationTeam.Value != RotationTeam.Lineup)
                        {
                            // sub inside same rotation team 
                            if ((RotationTeam)_firstSubstitute.RotationTeam.Value == (RotationTeam)_secondSubstitute.RotationTeam.Value)
                            {
                                if (Substitutions.Contains(_firstSubstitute))
                                {
                                    Substitutions.RemoveAt(firstSubIndex);
                                    Substitutions.Insert(firstSubIndex, _secondSubstitute);
                                    Substitutions.RemoveAt(secondSubIndex);
                                    Substitutions.Insert(secondSubIndex, _firstSubstitute);
                                }
                                else if (Reserves.Contains(_firstSubstitute))
                                {
                                    Reserves.RemoveAt(firstSubIndex);
                                    Reserves.Insert(firstSubIndex, _secondSubstitute);
                                    Reserves.RemoveAt(secondSubIndex);
                                    Reserves.Insert(secondSubIndex, _firstSubstitute);
                                }
                            }
                            // sub between differant rotaion team
                            else if ((RotationTeam)_firstSubstitute.RotationTeam.Value != (RotationTeam)_secondSubstitute.RotationTeam.Value)
                            {
                                if (Substitutions.Contains(_firstSubstitute))
                                {
                                    Substitutions.RemoveAt(firstSubIndex);
                                    Substitutions.Insert(firstSubIndex, _secondSubstitute);

                                    Reserves.RemoveAt(secondSubIndex);
                                    Reserves.Insert(secondSubIndex, _firstSubstitute);
                                }
                                else if (Reserves.Contains(_firstSubstitute))
                                {
                                    Reserves.RemoveAt(firstSubIndex);
                                    Reserves.Insert(firstSubIndex, _secondSubstitute);

                                    Substitutions.RemoveAt(secondSubIndex);
                                    Substitutions.Insert(secondSubIndex, _firstSubstitute);
                                }
                            }
                        }
                        // sub first sub from 'field details' to second sub in 'lineup details'
                        else if ((RotationTeam)_firstSubstitute.RotationTeam.Value == RotationTeam.Lineup && (RotationTeam)_secondSubstitute.RotationTeam.Value != RotationTeam.Lineup)
                        {
                            if (Substitutions.Contains(_secondSubstitute))
                            {
                                Substitutions.Remove(_secondSubstitute);
                                _firstSubstitute.RotationTeam.Value = RotationTeam.Substitute;
                                Substitutions.Insert(secondSubIndex, _firstSubstitute);
                            }
                            else if (Reserves.Contains(_secondSubstitute))
                            {
                                Reserves.Remove(_secondSubstitute);
                                _firstSubstitute.RotationTeam.Value = RotationTeam.Reserves;
                                Reserves.Insert(secondSubIndex, _firstSubstitute);
                            }

                            _secondSubstitute.X = firstSubX;
                            _secondSubstitute.Y = firstSubY;
                            _firstSubstitute.X = secondSubX;
                            _firstSubstitute.Y = secondSubY;
                        }
                        // sub first sub from 'lineup details' to second sub in 'field details'
                        else if ((RotationTeam)_firstSubstitute.RotationTeam.Value != RotationTeam.Lineup && (RotationTeam)_secondSubstitute.RotationTeam.Value == RotationTeam.Lineup)
                        {
                            if (Substitutions.Contains(_firstSubstitute))
                            {
                                Substitutions.Remove(_firstSubstitute);
                                _secondSubstitute.RotationTeam.Value = RotationTeam.Substitute;
                                Substitutions.Insert(firstSubIndex, _secondSubstitute);
                            }
                            else if (Reserves.Contains(_firstSubstitute))
                            {
                                Reserves.Remove(_firstSubstitute);
                                _secondSubstitute.RotationTeam.Value = RotationTeam.Reserves;
                                Reserves.Insert(firstSubIndex, _secondSubstitute);
                            }

                            _firstSubstitute.X = secondSubX;
                            _firstSubstitute.Y = secondSubY;
                            _secondSubstitute.X = firstSubX;
                            _secondSubstitute.Y = firstSubY;
                        }

                        Substitutions.RemoveFirstNames();
                        Substitutions.ArrangePositionRoleAsec();
                        Substitutions = new SquadList<SoccerPlayerViewModel>(Substitutions);

                        Reserves.RemoveFirstNames();
                        Reserves.ArrangePositionRoleAsec();
                        Reserves = new SquadList<SoccerPlayerViewModel>(Reserves);

                        // reset subs VM
                        _firstSubstitute.SetIsSelectedBinding(false);
                        _secondSubstitute.SetIsSelectedBinding(false);
                        _firstSubstitute = null;
                        _secondSubstitute = null;

                        // Also: 
                        // recreate TeamDetails & PlayerDetails
                        break;
                }
            }
        }
    }
}
