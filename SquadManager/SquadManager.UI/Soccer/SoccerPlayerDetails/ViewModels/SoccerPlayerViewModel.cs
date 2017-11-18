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
        public int TeamId { get; set; }
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

        public SoccerPlayerViewModel(SoccerPlayer model = null, CollectionFactory collections = null, IChangeManager changeManager = null)
        {
            if (model == null) return;
            Id = model.Id;
            TeamId = model.TeamId;
            Name = new EditableCellViewModel(model.Name, changeManager, model);
            Age = new CellViewModel(model.Age);
            BirthDate = new EditableCellViewModel(model.BirthDate.ToShortDateString(), changeManager, model);
            IsCaptain = new EditableCellViewModel(model.IsCaptain, changeManager, model);
            Position = new ComboBoxCellViewModel(collections.PositionRoles.Find(pr => pr == model.Position.Role), collections.PositionRoles, changeManager, model);
            Rating = new EditableCellViewModel(model.Rating, changeManager, model);
            Nationality = new ComboBoxCellViewModel(collections.NationViewModels.Find(n => n.Id == model.Nationality), collections.NationViewModels, changeManager, model);
            RotationTeam = new CellViewModel(model.Rotation);
            IsLineup = model.IsLineup;
            IsNewPlayer = new CellViewModel(false);
        }

        public SoccerPlayerViewModel(SoccerPlayerViewModel viewModel, CollectionFactory collections, IChangeManager changeManager, Application app)
        {
            if (viewModel == null) return;
            var playerModel = app.Teams.First(t => t.Id == viewModel.TeamId).Squad.First(p => p.Id == viewModel.Id);

            Id = viewModel.Id;
            TeamId = viewModel.TeamId;
            Name = new EditableCellViewModel(viewModel.Name.Value, changeManager, playerModel);

            DateTime birthDate = new DateTime();
            if (viewModel.BirthDate.Value is DateTime) birthDate = (DateTime)viewModel.BirthDate.Value;
            else birthDate = DateTime.Parse((string)viewModel.BirthDate.Value);
            BirthDate = new EditableCellViewModel(birthDate.ToShortDateString(), changeManager, playerModel);

            Age = new CellViewModel(DateTime.Now.Year - birthDate.Year);
            IsCaptain = new EditableCellViewModel(viewModel.IsCaptain.Value, changeManager, playerModel);
            Position = new ComboBoxCellViewModel(collections.PositionRoles.Find(pr => pr == (PositionRole)viewModel.Position.Value), collections.PositionRoles, changeManager, playerModel);
            Nationality = new ComboBoxCellViewModel(collections.NationViewModels.Find(n => n.Id == (viewModel.Nationality.Value as ComboBoxItemViewModel).Id), collections.NationViewModels, changeManager, playerModel);
            Rating = new EditableCellViewModel(viewModel.Rating.Value, changeManager, playerModel);
            RotationTeam = new CellViewModel(viewModel.RotationTeam.Value);
            IsNewPlayer = new CellViewModel(false);
            IsLineup = viewModel.IsLineup;
        }
    }
}
