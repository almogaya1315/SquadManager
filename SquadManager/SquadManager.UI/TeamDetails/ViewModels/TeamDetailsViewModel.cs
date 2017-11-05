﻿using System;
using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SquadManager.UI.Repositories;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamDetailsViewModel : ViewModel
    {
        public ICommand NewTeam { get; set; }

        public TeamDetailsViewModel()
        {
            NewTeam = new RelayCommand(GetTeam);
        }

        private void GetTeam()
        {
            SquadRepository.CreateNewTeam();
            
        }
    }
}