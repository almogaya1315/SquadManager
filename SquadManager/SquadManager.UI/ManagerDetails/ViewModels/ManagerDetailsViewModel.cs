﻿using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SquadManager.UI.ManagerDetails.ViewModels
{
    public class ManagerDetailsViewModel : ViewModel
    {
        private List<ManagerViewModel> _managers;
        public List<ManagerViewModel> Managers
        {
            get { return _managers; }
            set
            {
                _managers = value;
                RaisePropertyChanged();
            }
        }
        private ManagerViewModel _managerViewModel;
        public ManagerViewModel ManagerViewModel
        {
            get { return _managerViewModel; }
            set
            {
                _managerViewModel = value;
                if (ManagerViewModel != null)
                    ManagerViewModel.PropertyChanged += ManagerViewModel_PropertyChanged;
                RaisePropertyChanged();
            }
        }

        public ICommand Cancel { get; set; }
        public ICommand Save { get; set; }

        private bool _ageValidationVisible;
        public bool AgeValidationVisible
        {
            get { return _ageValidationVisible;; }
            set
            {
                _ageValidationVisible = value;
                RaisePropertyChanged();
            }
        }

        private bool _chooseManagerVisibility;
        public bool ChooseManagerVisibility
        {
            get { return _chooseManagerVisibility; }
            set
            {
                _chooseManagerVisibility = value;
                RaisePropertyChanged();
            }
        }

        public ManagerDetailsViewModel() { }
        public ManagerDetailsViewModel(Application app, CollectionFactory collection, ViewModelBrowser browser, ISquadRepository squadRepository)
        {
            App = app;
            Browser = browser;
            Collections = collection;
            SquadRepository = squadRepository;

            SetManager();

            ChooseManagerVisibility = Managers.Count > 0;

            Cancel = new RelayCommand(NavigateToMenu);
            Save = new RelayCommand(SaveManager, CanSave);
        }

        private void SetManager()
        {
            Managers = Collections.ManagerViewModels;

            ManagerViewModel = Managers.FirstOrDefault();

            if (ManagerViewModel != null)
                ManagerViewModel.PropertyChanged += ManagerViewModel_PropertyChanged;
            else ManagerViewModel = new ManagerViewModel();
        }

        private void ManagerViewModel_PropertyChanged(object sender, EventArgs e)
        {
            ManagerViewModel = null; 
            ManagerViewModel = new ManagerViewModel();

            Managers = Collections.ManagerViewModels;
        }

        private bool CanSave()
        {
            if (ManagerViewModel.Age.HasValue)
            {
                if (ManagerViewModel.Age.Value < 18)
                {
                    AgeValidationVisible = true;
                    return false;
                }
                else AgeValidationVisible = false;
            }

            return ManagerViewModel != null && !string.IsNullOrWhiteSpace(ManagerViewModel.Name) && 
                   ManagerViewModel.Nationality != null && !string.IsNullOrWhiteSpace(ManagerViewModel.Nationality.Name) && 
                   ManagerViewModel.Age.HasValue;
        }

        private void SaveManager()
        {
            Manager manager = null;
            if (!Managers.Exists(m => m.Id == ManagerViewModel.Id))
            {
                try
                {
                    manager = new Manager()
                    {
                        Name = ManagerViewModel.Name,
                        Nationality = new Nation() { Name = ManagerViewModel.Nationality.Name },
                        Age = ManagerViewModel.Age.Value,
                    };
                    App.Managers.Add(manager);

                    manager.Id = SquadRepository.AddManager(manager);
                }
                catch (Exception e)
                {
                    // TODO: validations
                }
            }

            manager = App.Managers.Find(m => m.Id == ManagerViewModel.Id);
            Browser.Browse(new TeamDetailsArgs(BrowseArgsType.TeamDetailsArgs, manager));
        }

        private void NavigateToMenu()
        {
            Browser.Browse(new BrowseArgs(BrowseArgsType.MenuArgs));
        }
    }
}
