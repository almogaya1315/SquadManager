using SquadManager.UI.Base;
using SquadManager.UI.Enums;
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

        private List<SoccerPlayerViewModel> _substitutions;
        public List<SoccerPlayerViewModel> Substitutions
        {
            get { return _substitutions; }
            set
            {
                _substitutions = value;
                RaisePropertyChanged();
            }
        }
        private List<SoccerPlayerViewModel> _reserves;
        public List<SoccerPlayerViewModel> Reserves
        {
            get { return _reserves; }
            set
            {
                _reserves = value;
                RaisePropertyChanged();
            }
        }

        public SoccerLineupDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _teamModel = team;

            Collections = collections;
            Team = new TeamViewModel(team, _changesManager, Collections);
            Substitutions = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Substitute).ToList();
            if (Substitutions.Count < 7) Substitutions.Add(new SoccerPlayerViewModel() { Name = new EditableCellViewModel(string.Empty, _changesManager), Position = new ComboBoxCellViewModel(null, null, _changesManager) });

            Reserves = Team.Squad.Where(p => (RotationTeam)p.RotationTeam.Value == RotationTeam.Reserves).ToList();

            string firstName = string.Empty;
            foreach (var player in Reserves)
            {
                foreach (var c in (string)player.Name.Value)
                {
                    if (Char.IsWhiteSpace(c)) break;
                    firstName += c;
                }
                var sureName = (player.Name.Value as string).Replace(firstName, string.Empty).TrimStart(new char[] { ' ' });
                player.Name.SetValueToBinding(sureName);
                firstName = string.Empty;
                sureName = string.Empty;
            }
        }

        public void Changed(ChangeArgs args)
        {

        }
    }
}
