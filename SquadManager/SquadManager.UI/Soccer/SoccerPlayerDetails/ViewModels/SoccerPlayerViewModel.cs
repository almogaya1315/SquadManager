using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels
{
    public class SoccerPlayerViewModel : ViewModel
    {
        public int Id { get; set; }
        public EditableCellViewModel Name { get; set; }
        public CellViewModel Age { get; set; }
        public EditableCellViewModel BirthDate { get; set; }
        public EditableCellViewModel IsCaptain { get; set; }
        public ComboBoxCellViewModel Position { get; set; }
        public ComboBoxCellViewModel Nationality { get; set; }
        public EditableCellViewModel Rating { get; set; }
        public CellViewModel RotationTeam { get; set; }
        public bool IsLineup { get; set; }
        public bool IsInjured { get; set; }
        public bool IsOnLoan { get; set; }
        public bool IsLoaned { get; set; }

        public CellViewModel IsNewPlayer { get; set; }

        public SoccerPlayerViewModel(SoccerPlayer model = null)
        {
            if (model == null) return;
            Id = model.Id;
            Name = new EditableCellViewModel(model.Name);
            Age = new CellViewModel(model.Age);
            BirthDate = new EditableCellViewModel(model.BirthDate);
            IsCaptain = new EditableCellViewModel(model.IsCaptain);
            Position = new ComboBoxCellViewModel(Collections.PositionRoles.Find(pr => pr == model.Position.Role), Collections.PositionRoles);
            Rating = new EditableCellViewModel(model.Rating);
            RotationTeam = new CellViewModel(model.Rotation);
            IsLineup = model.IsLineup;
        }

        public SoccerPlayerViewModel(SoccerPlayerViewModel viewModel, CollectionFactory collections)
        {
            if (viewModel == null) return;
            Id = viewModel.Id;
            Name = new EditableCellViewModel(viewModel.Name.Value);

            DateTime birthDate = new DateTime();
            if (viewModel.BirthDate.Value is DateTime) birthDate = (DateTime)viewModel.BirthDate.Value;
            else birthDate = DateTime.Parse((string)viewModel.BirthDate.Value);
            BirthDate = new EditableCellViewModel(birthDate.ToShortDateString());

            Age = new CellViewModel(viewModel.Age.Value);
            IsCaptain = new EditableCellViewModel(viewModel.IsCaptain.Value);
            Position = new ComboBoxCellViewModel(collections.PositionRoles.Find(pr => pr == (PositionRole)viewModel.Position.Value), collections.PositionRoles);
            Nationality = new ComboBoxCellViewModel(collections.NationViewModels.Find(n => n.Id == (viewModel.Nationality.Value as ComboBoxItemViewModel).Id), collections.NationViewModels);
            Rating = new EditableCellViewModel(viewModel.Rating.Value);
            RotationTeam = new CellViewModel(viewModel.RotationTeam.Value);
            IsNewPlayer = new CellViewModel(false);
            IsLineup = viewModel.IsLineup;
        }
    }
}
