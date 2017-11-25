using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class SoccerFieldDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changeManager;
        private Team _teamModel;

        public List<FormationViewModel> Formations { get; set; }
        public FormationViewModel SelectedFormation { get; set; }

        public SoccerFieldDetailsViewModel(Team team, IChangeManager changeManager, CollectionFactory collections)
        {
            _changeManager = changeManager;
            _teamModel = team;

            Collections = collections;

            Formations = new List<FormationViewModel>()
            {
                new FormationViewModel() { Name = "4-4-2", Player_1 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.GK), Collections, _changeManager),
                                                           Player_2 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.LB), Collections, _changeManager),
                                                           Player_3 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CB), Collections, _changeManager),
                                                           Player_4 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CB), Collections, _changeManager),
                                                           Player_5 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.RB), Collections, _changeManager),
                                                           Player_6 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.LW), Collections, _changeManager),
                                                           Player_7 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CM), Collections, _changeManager),
                                                           Player_8 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CM), Collections, _changeManager),
                                                           Player_9 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.RW), Collections, _changeManager),
                                                           Player_10 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CAM), Collections, _changeManager),
                                                           Player_11 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CF), Collections, _changeManager)},
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
            };

            SelectedFormation = Formations.First();
        }

        public void Changed(ChangeArgs args)
        {

        }
    }
}
