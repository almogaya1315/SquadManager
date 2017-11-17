﻿using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
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

        public TeamViewModel Team { get; set; }

        public ObservableCollection<SoccerPlayerViewModel> Players { get; set; }
        public SoccerPlayerViewModel NewPlayer { get; set; }

        public List<ColumnViewModel> Columns { get; set; }

        public ICommand AddNewPlayer { get; set; }

        public SoccerSquadDetailsViewModel(Team team, IChangeManager changesManager, CollectionFactory collections)
        {
            _changesManager = changesManager;
            _teamModel = team;

            Collections = collections;

            SetColumns();

            Team = new TeamViewModel(_teamModel, _changesManager, Collections);

            //var position = new Position() { Role = PositionRole.GK };
            NewPlayer = new SoccerPlayerViewModel()
            {
                Name = new EditableCellViewModel(null),
                BirthDate = new EditableCellViewModel(null),
                Age = new CellViewModel(null),
                Position = new ComboBoxCellViewModel(null, Collections.PositionRoles),
                Nationality = new ComboBoxCellViewModel(null, Collections.NationViewModels),
                IsCaptain = new EditableCellViewModel(false),
                IsNewPlayer = new CellViewModel(true),
            };
            Players = new ObservableCollection<SoccerPlayerViewModel>() { NewPlayer };
            Players.AddRange(Team.Squad);

            AddNewPlayer = new RelayCommand(AddNewPlayerToSquad, CanAdd);
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
                    Template = "AddNewPlayerButtonTemplate"
                },
            };
        }

        private bool CanAdd()
        {
            return NewPlayer.Position.Value != null && NewPlayer.Name.Value != null && NewPlayer.BirthDate.Value != null;
        }

        private void AddNewPlayerToSquad()
        {
            Players.Add(NewPlayer);
            NewPlayer = new SoccerPlayerViewModel()
            {
                Name = new EditableCellViewModel(null),
                BirthDate = new EditableCellViewModel(new DateTime()),
                Age = new CellViewModel(null),
                Position = new ComboBoxCellViewModel(null, Collections.PositionRoles),
                Nationality = new ComboBoxCellViewModel(null, Collections.NationViewModels),
                IsCaptain = new EditableCellViewModel(false),
                IsNewPlayer = new CellViewModel(true),
            };
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
