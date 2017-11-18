using SquadManager.UI.Enums;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Models
{
    public class SoccerPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age
        {
            get { return DateTime.Now.Year - BirthDate.Year; }
        }
        public DateTime BirthDate { get; set; }
        public bool IsCaptain { get; set; }
        public Position Position { get; set; }
        public int Rating { get; set; }
        public RotationTeam Rotation { get; set; }
        public int Nationality { get; set; }
        public bool IsLineup { get; set; }
        public bool IsInjured { get; set; }
        public bool IsOnLoan { get; set; }
        public bool IsLoaned { get; set; }

        public SoccerPlayer() { }
        public SoccerPlayer(SoccerPlayerViewModel viewModel = null)
        {
            if (viewModel == null) return;

            Name = (string)viewModel.Name.Value;

            DateTime birthDate = new DateTime();
            if (viewModel.BirthDate.Value is DateTime) birthDate = (DateTime)viewModel.BirthDate.Value;
            else birthDate = DateTime.Parse((string)viewModel.BirthDate.Value);
            BirthDate = birthDate;

            Position =  CreatePosition((PositionRole)viewModel.Position.Value);

            IsCaptain = (bool)viewModel.IsCaptain.Value;
            Rating = int.Parse((string)viewModel.Rating.Value);
            Rotation = (RotationTeam)viewModel.RotationTeam.Value;
            Nationality = (viewModel.Nationality.Value as ComboBoxItemViewModel).Id;
        }

        private Position CreatePosition(PositionRole role)
        {
            switch (role)
            {
                case PositionRole.GK: return new Position() { Group = PositionGroup.GoalKeepers, Role = role };
                case PositionRole.RB:
                case PositionRole.RWB:
                case PositionRole.CB:
                case PositionRole.LWB:
                case PositionRole.LB: return new Position() { Group = PositionGroup.Defenders, Role = role };
                case PositionRole.RDM:
                case PositionRole.CDM:
                case PositionRole.LDM:
                case PositionRole.LM:
                case PositionRole.LWM:
                case PositionRole.CM:
                case PositionRole.RWM:
                case PositionRole.RM:
                case PositionRole.RCAM:
                case PositionRole.CAM:
                case PositionRole.LCAM: return new Position() { Group = PositionGroup.Midfielders, Role = role };
                case PositionRole.RW:
                case PositionRole.LW:
                case PositionRole.RF:
                case PositionRole.CF:
                case PositionRole.LF:
                case PositionRole.RS:
                case PositionRole.ST:
                case PositionRole.LS: return new Position() { Group = PositionGroup.Attackers, Role = role };
            }

            throw new InvalidOperationException();
        }
    }
}
