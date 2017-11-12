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
        public EditableCellViewModel Position { get; set; }
        public bool IsLineup { get; set; }

        public SoccerPlayerViewModel() { }
        public SoccerPlayerViewModel(SoccerPlayer model)
        {
            Id = model.Id;
            Name = new EditableCellViewModel(model.Name);
            Age = new CellViewModel(model.Age);
            BirthDate = new EditableCellViewModel(model.BirthDate);
            IsCaptain = new EditableCellViewModel(model.IsCaptain);
            Position = new EditableCellViewModel(model.Position);
            IsLineup = model.IsLineup;
        }
    }
}
