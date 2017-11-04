using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
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

        public ManagerDetailsViewModel() { }
        public ManagerDetailsViewModel(Application app, CollectionFactory collections)
        {
            App = app;
            Collections = collections;

            SetManager();

            Cancel = new RelayCommand(NavigateToMenu);
            Save = new RelayCommand(SaveManager, CanSave);
        }

        private void SetManager()
        {
            Managers = SetManagers();

            ManagerViewModel = Managers.First();

            ManagerViewModel.PropertyChanged += ManagerViewModel_PropertyChanged;
        }

        private List<ManagerViewModel> SetManagers()
        {
            return App.Managers.Select(m => new ManagerViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Nationality = Collections.NationViewModels.Find(n => n.Id == m.Nationality.Id),
                Age = m.Age
            }).ToList();
        }

        private void ManagerViewModel_PropertyChanged(object sender, EventArgs e)
        {
            ManagerViewModel = null; 
            ManagerViewModel = new ManagerViewModel();

            Managers = SetManagers();
        }

        private bool CanSave()
        {
            return ManagerViewModel != null && !string.IsNullOrWhiteSpace(ManagerViewModel.Name) && 
                   ManagerViewModel.Nationality != null && !string.IsNullOrWhiteSpace(ManagerViewModel.Nationality.Name) && 
                   ManagerViewModel.Age.HasValue;
        }

        private void SaveManager()
        {
            if (!Managers.Exists(m => m.Id == ManagerViewModel.Id))
            {
                try
                {
                    App.Managers.Add(new Manager()
                    {
                        Name = ManagerViewModel.Name,
                        Nationality = new Nation() { Name = ManagerViewModel.Nationality.Name },
                        Age = ManagerViewModel.Age.Value,
                    });

                    SquadRepository.AddManager(ManagerViewModel);
                }
                catch (Exception e)
                {
                    // TODO: validations
                }
            }

            Browser.Browse(new BrowseArgs(ArgsType.TeamDetailsArgs));
        }

        private void NavigateToMenu()
        {
            Browser.Browse(new BrowseArgs(ArgsType.MenuArgs));
        }
    }
}
