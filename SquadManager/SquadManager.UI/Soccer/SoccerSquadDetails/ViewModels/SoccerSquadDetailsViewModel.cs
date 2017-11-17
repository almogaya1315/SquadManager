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

            var position = new Position() { Role = PositionRole.CAM };
            NewPlayer = new SoccerPlayerViewModel()
            {
                Name = new EditableCellViewModel("New player"),
                BirthDate = new EditableCellViewModel(new DateTime(1985, 5, 23).ToShortDateString()),
                Age = new CellViewModel(18),
                Position = new ComboBoxCellViewModel(Collections.PositionRoles.Find(pr => pr == position.Role), Collections.PositionRoles),
                Nationality = new ComboBoxCellViewModel(Collections.NationViewModels.Find(n => n.Id == 1), Collections.NationViewModels),
                IsCaptain = new EditableCellViewModel(true),
            };
            Players = new List<SoccerPlayerViewModel>() { NewPlayer };
            Players.AddRange(Team.Squad);

            var NewPlayer1 = new SoccerPlayerViewModel()
            {
                Name = new EditableCellViewModel("New player"),
                BirthDate = new EditableCellViewModel(new DateTime(1985, 5, 23).ToShortDateString()),
                Age = new CellViewModel(18),
                Position = new ComboBoxCellViewModel(Collections.PositionRoles.Find(pr => pr == position.Role), Collections.PositionRoles),
                Nationality = new ComboBoxCellViewModel(Collections.NationViewModels.Find(n => n.Id == 2), Collections.NationViewModels),
                IsCaptain = new EditableCellViewModel(false),
            };
            Players.Add(NewPlayer1);
        }

        private void SetColumns()
        {
            Columns = new List<ColumnViewModel>()
            {
                new ColumnViewModel()
                {
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "IsCaptain",
                    Template = "RadioButtonEditingTemplate", //  "ImageReadOnlyCellTemplate"
                    EditingTemplate = "RadioButtonEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Position",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Position",
                    Template = "ComboBoxReadOnlyCellTemplate", // "ComboBoxEditingTemplate"
                    EditingTemplate = "ComboBoxCellEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Name",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Name",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "TextEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Rating",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Rating",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "NumericEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Birth date",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "BirthDate",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "CalendarEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Age",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Age",
                    Template = "ReadOnlyCellTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "Nationality",
                    HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Nationality",
                    Template = "ComboBoxItemReadOnlyCellTemplate", // "ComboBoxReadOnlyCellTemplate"
                    EditingTemplate = "ComboBoxItemCellEditingTemplate", // "ComboBoxCellEditingTemplate"
                },
            };
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
