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
        private readonly IChangeManager _changesManager;
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

        private SoccerPlayerViewModel _firstSubstitute;
        public SoccerPlayerViewModel FirstSubstitute
        {
            get { return _firstSubstitute; }
            set
            {
                _firstSubstitute = value;
                RaisePropertyChanged();
            }
        }
        private SoccerPlayerViewModel _secondSubstitute;
        public SoccerPlayerViewModel SecondSubstitute
        {
            get { return _secondSubstitute; }
            set
            {
                _secondSubstitute = value;
                RaisePropertyChanged();
            }
        }

        public SoccerLineupDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _teamModel = team;

            Collections = collections;
            Team = new TeamViewModel(team, _changesManager, Collections);

            SoccerPlayerViewModel.IsSelectedPropertyChanged += SoccerPlayerViewModel_IsSelectedPropertyChanged;

            for (int i = 0; i < 7; i++) Team.Squad.ElementAt(i).RotationTeam.Value = RotationTeam.Substitute;
            for (int i = 7; i < 18; i++) Team.Squad.ElementAt(i).RotationTeam.Value = RotationTeam.Lineup;
            Substitutions = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Substitute).ToSquadList();
            if (Substitutions.Count < 7) Substitutions.Add(new SoccerPlayerViewModel() { Name = new EditableCellViewModel(string.Empty, _changesManager), Position = new ComboBoxCellViewModel(null, null, _changesManager) });
            Reserves = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Reserves).ToSquadList();
            Reserves.RemoveAll(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Lineup);

            Reserves.RemoveFirstNames();
            Substitutions.RemoveFirstNames();
        }

        private void SoccerPlayerViewModel_IsSelectedPropertyChanged(object sender, IsSelectedEventArgs args)
        {
            var player = (SoccerPlayerViewModel)sender;

            if (args.IsSelected)
            {
                if (FirstSubstitute == null)
                {
                    FirstSubstitute = player;
                    _changesManager.Change(new SubstitutionArgs(ChangeType.SubSelected, new SoccerPlayer(FirstSubstitute)));
                }
                else if (FirstSubstitute != null && SecondSubstitute == null)
                {
                    SecondSubstitute = player;
                    _changesManager.Change(new SubstitutionArgs(ChangeType.SubSelected, new SoccerPlayer(FirstSubstitute), new SoccerPlayer(SecondSubstitute)));
                }
                else if (FirstSubstitute != null && SecondSubstitute != null) player.SetIsSelectedBinding(false);
            }
            else
            {
                if (FirstSubstitute != null && FirstSubstitute.Id == player.Id)
                {
                    FirstSubstitute = null;
                    _changesManager.Change(new SubstitutionArgs(ChangeType.SubDeselect, null, SecondSubstitute != null ? new SoccerPlayer(SecondSubstitute) : null));
                }
                else if (SecondSubstitute != null && SecondSubstitute.Id == player.Id)
                {
                    SecondSubstitute = null;
                    _changesManager.Change(new SubstitutionArgs(ChangeType.SubDeselect, FirstSubstitute != null ? new SoccerPlayer(FirstSubstitute) : null, null));
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
                    if (FirstSubstitute == null)
                    {
                        FirstSubstitute = new SoccerPlayerViewModel(firstSub, Collections, _changesManager);
                    }
                    else if (firstSub.Id == FirstSubstitute.Id && secondSub == null) return;

                    if (secondSub != null)
                    {
                        if (SecondSubstitute == null)
                        {
                            SecondSubstitute = new SoccerPlayerViewModel(secondSub, Collections, _changesManager);
                        }
                        else if (secondSub.Id == SecondSubstitute.Id) return;
                    }
                }
                else if (args.Type == ChangeType.SubDeselect)
                {
                    if (firstSub != null)
                    {
                        FirstSubstitute = null;
                    }
                    else if (secondSub != null)
                    {
                        SecondSubstitute = null;
                    }
                }
                else if (args.Type == ChangeType.SubConfirmed)
                {
                    // TODO:

                    // reset subs VM

                    // Also: 
                    // recreate TeamDetails & PlayerDetails
                }
            }
        }
    }
}
