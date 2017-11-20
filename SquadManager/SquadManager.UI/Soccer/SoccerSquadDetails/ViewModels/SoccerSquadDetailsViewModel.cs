using GalaSoft.MvvmLight.CommandWpf;
using Mosaic.UI.Extensions;
using SquadManager.UI.Base;
using SquadManager.UI.Enums;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SquadManager.UI.Soccer.SoccerSquadDetails.ViewModels
{
    public class SoccerSquadDetailsViewModel : ViewModel, IChangeable
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

        public ObservableCollection<SoccerPlayerViewModel> Players { get; set; }

        private SoccerPlayerViewModel _newPlayer;
        public SoccerPlayerViewModel NewPlayer
        {
            get { return _newPlayer; }
            set
            {
                _newPlayer = value;
                RaisePropertyChanged();
            }
        }

        private SoccerPlayerViewModel _selectedPlayer;
        public SoccerPlayerViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;

                if (value != null) _changesManager.Change(new SoccerPlayerArgs(_teamModel.Squad.Find(p => p.Id == (value as SoccerPlayerViewModel).Id), ChangeType.PlayerSelected));
            }
        }

        private SoccerPlayerViewModel _playerToRemove;
        public SoccerPlayerViewModel PlayerToRemove
        {
            get { return _playerToRemove; }
            set
            {
                _playerToRemove = value;
                RaisePropertyChanged();
            }
        }
        private bool _confirmationOverlayVisible;
        public bool ConfirmationOverlayVisible
        {
            get { return _confirmationOverlayVisible; }
            set
            {
                _confirmationOverlayVisible = value;
                RaisePropertyChanged();
            }
        }
        private bool _panelEnabled;
        public bool PanelEnabled
        {
            get { return _panelEnabled; }
            set
            {
                _panelEnabled = value;
                RaisePropertyChanged();
            }
        }

        public List<ColumnViewModel> Columns { get; set; }

        public ICommand AddNewPlayer { get; set; }
        public ICommand RemovePlayerConfirmation { get; set; }
        public ICommand RemovePlayerConfirmed { get; set; }

        public SoccerSquadDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections, ISquadRepository squadRepository, Application app)
        {
            _changesManager = changesManager;
            _teamModel = team;

            App = app;
            SquadRepository = squadRepository;
            Collections = collections;

            SetColumns();
            SetPlayers();

            AddNewPlayer = new RelayCommand(AddNewPlayerToSquad, CanAdd);
            RemovePlayerConfirmation = new RelayCommand<SoccerPlayerViewModel>(ExecuteRemovePlayerConfirmation);
            RemovePlayerConfirmed = new RelayCommand<bool>(ExecuteRemovePlayerConfirmed);

            PanelEnabled = true;
            ConfirmationOverlayVisible = false;
        }

        private void SetPlayers()
        {
            Team = new TeamViewModel(_teamModel, _changesManager, Collections);
           
            Players = new ObservableCollection<SoccerPlayerViewModel>();
            Team.Squad.ForEach(p => Players.Add(p));

            NewPlayer = new SoccerPlayerViewModel()
            {
                Name = new EditableCellViewModel(null, _changesManager),
                BirthDate = new EditableCellViewModel(new DateTime(1950, 1, 1).ToShortDateString(), _changesManager),
                Age = new CellViewModel(null),
                Position = new ComboBoxCellViewModel(null, Collections.PositionRoles, _changesManager),
                Nationality = new ComboBoxCellViewModel(null, Collections.NationViewModels, _changesManager),
                Rating = new EditableCellViewModel(null, _changesManager),
                RotationTeam = new CellViewModel(RotationTeam.Reserves),
                IsCaptain = new EditableCellViewModel(false, _changesManager),
                IsNewPlayer = new CellViewModel(true),
            };
            Players.Add(NewPlayer);
        }

        private void SetColumns()
        {
            Columns = new List<ColumnViewModel>()
            {
                new ColumnViewModel()
                {
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "IsCaptain",
                    Template = "RadioButtonEditingTemplate",
                    EditingTemplate = "RadioButtonEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "NO.",
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "PlayerNumber",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "NumericEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "POSITION",
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Position",
                    Template = "ComboBoxReadOnlyCellTemplate",
                    EditingTemplate = "ComboBoxCellEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "NAME",
                    Width = 150.0,
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Name",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "TextEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "RATING",
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Rating",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "NumericEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "BIRTH DATE",
                    Width = 115.0,
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "BirthDate",
                    Template = "ReadOnlyCellTemplate",
                    EditingTemplate = "CalendarEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "AGE",
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Age",
                    Template = "ReadOnlyCellTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "NATIONALITY",
                    //HeaderTemplate = "DefualtHeaderTemplate",
                    DataContextPath = "Nationality",
                    Template = "ComboBoxItemReadOnlyCellTemplate",
                    EditingTemplate = "ComboBoxItemCellEditingTemplate",
                },
                new ColumnViewModel()
                {
                    Header = "ROTATION",
                    Width = 100.0,
                    DataContextPath = "RotationTeam",
                    Template = "ReadOnlyCellTemplate",
                },
                new ColumnViewModel()
                {
                    DataContextPath = "IsNewPlayer",
                    Template = "AddRemovePlayerButtonTemplate"
                },
            };
        }

        private bool CanAdd()
        {
            var rating = NewPlayer.Rating.Value != null ? int.Parse((string)NewPlayer.Rating.Value) : 0;

            return NewPlayer.Position.Value != null && NewPlayer.Name.Value != null && NewPlayer.BirthDate.Value != null && 
                   NewPlayer.Rating.Value != null && rating > 0 && rating <= 100;
        }

        private void AddNewPlayerToSquad()
        {
            var player = new SoccerPlayerViewModel(NewPlayer, Collections, _changesManager, App);
            Players.Remove(NewPlayer);
            Players.Add(player);

            ResetNewPlayerValues();
            Players.Add(NewPlayer);

            Team.Squad.Add(player);
            var playerModel = new SoccerPlayer(player);
            _teamModel.Squad.Add(playerModel);
            

            playerModel.Id = player.Id = SquadRepository.AddPlayer(_teamModel.Id, _teamModel.Squad.Last());

            _changesManager.Change(new SoccerPlayerArgs(playerModel, ChangeType.PlayerAdded));
        }

        private void ResetNewPlayerValues()
        {
            NewPlayer.Name.Value = null;
            NewPlayer.BirthDate.Value = new DateTime(1950, 1, 1).ToShortDateString();
            NewPlayer.Age.Value = null;
            NewPlayer.Position.Value = null;
            NewPlayer.Nationality.Value = null;
            NewPlayer.Rating.Value = null;
            NewPlayer.IsCaptain.Value = false;
            NewPlayer.IsNewPlayer.Value = true;
        }

        private void ExecuteRemovePlayerConfirmation(SoccerPlayerViewModel player)
        {
            PlayerToRemove = player;
            PanelEnabled = false;
            ConfirmationOverlayVisible = true;
        }

        private void ExecuteRemovePlayerConfirmed(bool isConfirmed)
        {
            if (isConfirmed)
            {
                Players.Remove(PlayerToRemove);
                Team.Squad.Remove(PlayerToRemove);
                var playerModel = _teamModel.Squad.Find(p => p.Id == PlayerToRemove.Id);
                _teamModel.Squad.Remove(playerModel);

                SquadRepository.DeletePlayer(_teamModel.Id, PlayerToRemove.Id);

                _changesManager.Change(new SoccerPlayerArgs(playerModel, ChangeType.PlayerDeleted));
            }

            PlayerToRemove = null;
            PanelEnabled = true;
            ConfirmationOverlayVisible = false;
        }

        public void Changed(ChangeArgs args)
        {
            if (args.Type != ChangeType.PlayerChanged) return;

            switch (args.Type)
            {
                case ChangeType.PlayerChanged:
                    var changedPlayerArgs = (SoccerPlayerArgs)args;
                    var player = changedPlayerArgs.PlayerValues;
                    if (changedPlayerArgs.Column == ColumnName.IsCaptain)
                        player.IsCaptain = (bool)Team.Squad.Find(p => p.Id == player.Id).IsCaptain.Value;

                    // TODO: Correct position group value to DB
                    if (changedPlayerArgs.Column == ColumnName.Position)
                        player.Position.Role = (PositionRole)Team.Squad.Find(p => p.Id == player.Id).Position.Value;

                    SquadRepository.UpdatePlayer(_teamModel.Id, player);

                    break;
                default:
                    break;
            }
        }
    }
}
