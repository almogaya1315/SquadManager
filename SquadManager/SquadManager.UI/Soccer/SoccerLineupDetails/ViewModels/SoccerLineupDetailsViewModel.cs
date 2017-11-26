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
                if (FirstSubstitute == null) FirstSubstitute = player;
                else if (FirstSubstitute != null && SecondSubstitute == null) SecondSubstitute = player;
                else if (FirstSubstitute != null && SecondSubstitute != null) player.SetIsSelectedBinding(false);
            }
            else
            {
                if (FirstSubstitute != null && FirstSubstitute.Id == player.Id) FirstSubstitute = null;
                else if (SecondSubstitute != null && SecondSubstitute.Id == player.Id) SecondSubstitute = null;
            }
        }

        public void Changed(ChangeArgs args)
        {

        }
    }
}
