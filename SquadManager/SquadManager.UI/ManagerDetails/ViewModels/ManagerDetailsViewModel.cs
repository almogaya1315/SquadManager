using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
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
        public ManagerViewModel ManagerViewModel { get; set; }

        public ICommand Cancel { get; set; }
        public ICommand Save { get; set; }

        public ManagerDetailsViewModel() { }
        public ManagerDetailsViewModel(CollectionFactory collections)
        {
            Collections = collections;

            ManagerViewModel = new ManagerViewModel();
            ManagerViewModel.Nationality = Collections.NationViewModels.First();

            Cancel = new RelayCommand(NavigateToMenu);
            Save = new RelayCommand(SaveManager, CanSave);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ManagerViewModel.Name) && !string.IsNullOrWhiteSpace(ManagerViewModel.Nationality.Name) && ManagerViewModel.Age.HasValue;
        }

        private void SaveManager()
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

        private void NavigateToMenu()
        {
            Manager.Browse(new BrowseArgs(ArgsType.MenuArgs));
        }
    }
}
