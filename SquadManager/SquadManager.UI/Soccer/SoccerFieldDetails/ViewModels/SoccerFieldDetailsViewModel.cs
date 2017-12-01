using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class SoccerFieldDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changeManager;
        private Team _teamModel;

        public List<FormationViewModel> Formations { get; set; }
        public FormationViewModel SelectedFormation { get; set; }

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

        public ICommand Substitute { get; set; }

        public SoccerFieldDetailsViewModel(Team team, IChangeManager changeManager, CollectionFactory collections, ISquadRepository squadRepository)
        {
            _changeManager = changeManager;
            _teamModel = team;

            SquadRepository = squadRepository;
            Collections = collections;

            var defaultFormation = SquadRepository.GetDefaultFormation();

            Formations = new List<FormationViewModel>()
            {
                new FormationViewModel() { Name = "4-4-2", Player_1 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.GK), Collections, _changeManager) { X = defaultFormation.Player_1X, Y = defaultFormation.Player_1Y},
                                                           Player_2 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.LB), Collections, _changeManager) { X = defaultFormation.Player_2X, Y = defaultFormation.Player_2Y},
                                                           Player_3 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CB), Collections, _changeManager) { X = defaultFormation.Player_3X, Y = defaultFormation.Player_3Y},
                                                           Player_4 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CB), Collections, _changeManager) { X = defaultFormation.Player_4X, Y = defaultFormation.Player_4Y},
                                                           Player_5 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.RB), Collections, _changeManager) { X = defaultFormation.Player_5X, Y = defaultFormation.Player_5Y},
                                                           Player_6 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.LW), Collections, _changeManager) { X = defaultFormation.Player_6X, Y = defaultFormation.Player_6Y},
                                                           Player_7 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CM), Collections, _changeManager) { X = defaultFormation.Player_7X, Y = defaultFormation.Player_7Y},
                                                           Player_8 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CM), Collections, _changeManager) { X = defaultFormation.Player_8X, Y = defaultFormation.Player_8Y},
                                                           Player_9 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.RW), Collections, _changeManager) { X = defaultFormation.Player_9X, Y = defaultFormation.Player_9Y},
                                                           Player_10 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.CAM), Collections, _changeManager) { X = defaultFormation.Player_10X, Y = defaultFormation.Player_10Y},
                                                           Player_11 = new SoccerPlayerViewModel(_teamModel.Squad.FirstOrDefault(p => p.Position.Role == PositionRole.ST), Collections, _changeManager) { X = defaultFormation.Player_11X, Y = defaultFormation.Player_11Y},
                },

                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
            };

            SelectedFormation = Formations.First();
            SelectedFormation.Lineup.RemoveFirstNames();

            Substitute = new RelayCommand(SubstitutePlayers, CanSubstitute);
        }

        private bool CanSubstitute()
        {
            return FirstSubstitute != null && SecondSubstitute != null;
        }

        private void SubstitutePlayers()
        {
            var first = FirstSubstitute;
            var second = SecondSubstitute;

            // TODO: 

            // viewModel


            // model
            // DB
            // change
            // reset subs VM
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
                        FirstSubstitute = new SoccerPlayerViewModel(firstSub, Collections, _changeManager);
                    }
                    else if (firstSub.Id == FirstSubstitute.Id && secondSub == null) return;

                    if (secondSub != null)
                    {
                        if (SecondSubstitute == null)
                        {
                            SecondSubstitute = new SoccerPlayerViewModel(secondSub, Collections, _changeManager);
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
            }
        }
    }
}
