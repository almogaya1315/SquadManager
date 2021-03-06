﻿using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Repositories;
using SquadManager.UI.ManagerDetails.ViewModels;
using System.Collections.Generic;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using SquadManager.UI.Enums;
using System.Linq;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamDetailsViewModel : ViewModel
    {
        public TeamViewModel Team { get; set; }

        public List<ManagerViewModel> Managers { get; set; }

        public ICommand Back { get; set; }
        public ICommand Save { get; set; }

        public TeamDetailsViewModel() { }
        public TeamDetailsViewModel(Manager manager, Application app, ISquadRepository squadRepository, CollectionFactory collections, ViewModelBrowser browser)
        {
            App = app;
            SquadRepository = squadRepository;
            Collections = collections;
            Browser = browser;

            Managers = Collections.ManagerViewModels;

            Team = new TeamViewModel() { Manager = Managers.Find(m => m.Id == manager.Id) };

            Back = new RelayCommand(BackToManagerDetails);
            Save = new RelayCommand(SaveTeam, CanSave);
        }

        private bool CanSave()
        {
            return Team.Name != null && Team.Nation != null && Team.City != null && Team.Sport != null;
        }

        private void SaveTeam()
        {
            var team = new Team()
            {
                ManagerId = Team.Manager.Id,
                Name = Team.Name,
                NationId = Team.Nation.Id,
                CityId = Team.City.Id,
                SportId = Team.Sport.Id,
            };

            team.Id = SquadRepository.AddTeam(team);
            App.Teams.Add(team);

            switch (Enum.GetValues(typeof(SportType)).Cast<SportType>().First(t => t.ToString() == Team.Sport.Name))
            {
                case SportType.Soccer:
                    Browser.Browse(new SoccerArgs(BrowseArgsType.SoccerSquadArgs, team));
                    break;
            }   
        }

        private void BackToManagerDetails()
        {
            Browser.Browse(new BrowseArgs(BrowseArgsType.ManagerDetailsArgs));
        }
    }
}
