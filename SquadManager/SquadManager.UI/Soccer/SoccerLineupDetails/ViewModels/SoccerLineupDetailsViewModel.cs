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
            Reserves = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Reserves).ToSquadList();

            Reserves.RemoveFirstNames();
            Substitutions.RemoveFirstNames();
        }

        private void SoccerPlayerViewModel_IsSelectedPropertyChanged(object sender, IsSelectedEventArgs args)
        {
            var player = (SoccerPlayerViewModel)sender;

            if (args.IsSelected)
            {
                if (_firstSubstitute == null)
                {
                    _firstSubstitute = player;
                    _changeManager.Change(new SubstitutionArgs(ChangeType.SubSelected, _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy()));
                }
                else if (_firstSubstitute != null && _secondSubstitute == null)
                {
                    _secondSubstitute = player;
                    _changeManager.Change(new SubstitutionArgs(ChangeType.SubSelected, _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy(), _teamModel.Squad.Find(p => p.Id == _secondSubstitute.Id).RefferenceCopy()));
                }
                else if (_firstSubstitute != null && _secondSubstitute != null) player.SetIsSelectedBinding(false);
            }
            else
            {
                if (_firstSubstitute != null && _firstSubstitute.Id == player.Id)
                {
                    _firstSubstitute = null;
                    _changeManager.Change(new SubstitutionArgs(ChangeType.SubDeselect, null, _secondSubstitute != null ? _teamModel.Squad.Find(p => p.Id == _secondSubstitute.Id).RefferenceCopy() : null));
                }
                else if (_secondSubstitute != null && _secondSubstitute.Id == player.Id)
                {
                    _secondSubstitute = null;
                    _changeManager.Change(new SubstitutionArgs(ChangeType.SubDeselect, _firstSubstitute != null ? _teamModel.Squad.Find(p => p.Id == _firstSubstitute.Id).RefferenceCopy() : null, null));
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
                else if (args.Type == ChangeType.SubConfirmed)
                {
                    var firstSubIndex = Substitutions.Contains(_firstSubstitute) ? Substitutions.IndexOf(_firstSubstitute) : Reserves.IndexOf(_firstSubstitute);
                    var secondSubIndex = Substitutions.Contains(_secondSubstitute) ? Substitutions.IndexOf(_secondSubstitute) : Reserves.IndexOf(_secondSubstitute);

                    // sub inside 'lineup details'
                    if ((RotationTeam)_firstSubstitute.RotationTeam.Value != RotationTeam.Lineup && (RotationTeam)_secondSubstitute.RotationTeam.Value != RotationTeam.Lineup)
                    {
                        // sub inside same rotation team 
                        if (_firstSubstitute.RotationTeam.Value == _secondSubstitute.RotationTeam.Value)
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
                        else if (_firstSubstitute.RotationTeam.Value != _secondSubstitute.RotationTeam.Value)
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
                            Substitutions.Insert(secondSubIndex, _firstSubstitute);
                            Substitutions.RemoveFirstNames();
                        }
                        else if (Reserves.Contains(_secondSubstitute))
                        {
                            Reserves.Remove(_secondSubstitute);
                            Reserves.Insert(secondSubIndex, _firstSubstitute);
                            Reserves.RemoveFirstNames();
                        }
                    }
                    // sub first sub from 'lineup details' to second sub in 'field details'
                    else if ((RotationTeam)_firstSubstitute.RotationTeam.Value != RotationTeam.Lineup && (RotationTeam)_secondSubstitute.RotationTeam.Value == RotationTeam.Lineup)
                    {
                        if (Substitutions.Contains(_firstSubstitute))
                        {
                            Substitutions.Remove(_firstSubstitute);
                            Substitutions.Insert(firstSubIndex, _secondSubstitute);
                            Substitutions.RemoveFirstNames();
                        }
                        else if (Reserves.Contains(_firstSubstitute))
                        {
                            Reserves.Remove(_firstSubstitute);
                            Reserves.Insert(firstSubIndex, _secondSubstitute);
                            Reserves.RemoveFirstNames();
                        }
                    }

                    // reset subs VM
                    _firstSubstitute.SetIsSelectedBinding(false);
                    _secondSubstitute.SetIsSelectedBinding(false);
                    _firstSubstitute = null;
                    _secondSubstitute = null;

                    // Also: 
                    // recreate TeamDetails & PlayerDetails
                }
            }
        }
    }
}
