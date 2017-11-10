using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SquadManager.UI.LoadTeam.ViewModels
{
    public class LoadTeamViewModel : ViewModel
    {
        public List<TeamViewModel> Teams { get; set; }

        private TeamViewModel _selectedTeam;
        public TeamViewModel SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Load { get; set; }
        public ICommand Back { get; set; }

        public LoadTeamViewModel() { }
        public LoadTeamViewModel(Application app, ISquadRepository squadRepository, CollectionFactory collection)
        {
            App = app;
            SquadRepository = squadRepository;
            Collections = collection;

            Teams = collection.TeamViewModels;
            SelectedTeam = new TeamViewModel();

            Load = new RelayCommand(LoadTeam, () => SelectedTeam.Id > 0);
            Back = new RelayCommand(BackToMenu);
        }

        private void BackToMenu()
        {
            Browser.Browse(new BrowseArgs(BrowseArgsType.MenuArgs));
        }

        private void LoadTeam()
        {
            var team =  SquadRepository.GetTeam(SelectedTeam.Id);
        }
    }
}
