using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerSquadDetails.ViewModels
{
    public class SoccerSquadDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;

        private Team _teamModel;

        public TeamViewModel Team { get; set; }

        public List<SoccerPlayerViewModel> Players { get; set; }
        public SoccerPlayerViewModel NewPlayer { get; set; }

        public List<ColumnViewModel> Columns { get; set; }

        public SoccerSquadDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _teamModel = team;

            Collections = collections;

            SetColumns();

            Team = new TeamViewModel(_teamModel, _changesManager, Collections);

            NewPlayer = new SoccerPlayerViewModel();
            Players = new List<SoccerPlayerViewModel>() { NewPlayer };
            Players.AddRange(Team.Squad);
        }

        private void SetColumns()
        {
            Columns = new List<ColumnViewModel>()
            {
                new ColumnViewModel()
                {
                    Header = "Position",
                    DataContextPath = "Position",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "ComboBoxEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Name",
                    DataContextPath = "Name",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "TextEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Age",
                    DataContextPath = "Age",
                    Template = "ReadOnlyCellTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Captain",
                    DataContextPath = "IsCaptain",
                    Template = "ImageReadOnlyCellTemplate",
                    EditingTemplate = "RadioButtonEditingTemplate",
                },
            };
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
