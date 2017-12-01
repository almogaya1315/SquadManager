using GalaSoft.MvvmLight.CommandWpf;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Base;
using SquadManager.UI.Enums;
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
        public LoadTeamViewModel(Application app, ISquadRepository squadRepository, CollectionFactory collection, ViewModelBrowser browser)
        {
            App = app;
            Browser = browser;
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
            var sport = Enum.GetValues(typeof(SportType)).Cast<SportType>().First(s => s.ToString() == SelectedTeam.Sport.Name);
            var team = App.Teams.Find(t => t.Id == SelectedTeam.Id);
            team.Squad = SquadRepository.GetTeamSquad(team.Id);
            team.Formations = SetTeamFormations(team);

            switch (sport)
            {
                case SportType.Soccer:
                    Browser.Browse(new SoccerArgs(BrowseArgsType.SoccerSquadArgs, team));
                    break;
            }
        }

        private List<Formation> SetTeamFormations(Team team)
        {
            // TODO: should be in 'create new team' stage of the app

            var formations = SquadRepository.GetTeamFormations(team.Id);
            if (formations.Count == 1 && formations.First().Name.Contains("Default") && !formations.First().TeamId.HasValue)
            {
                var formation = formations.First();
                formation.TeamId = team.Id;
                formation.Name = "4-4-2";
                formation.Player_1Id = team.Squad.ElementAt(0) != null ? (int?)team.Squad.ElementAt(0).Id : null;
                formation.Player_2Id = team.Squad.ElementAt(1) != null ? (int?)team.Squad.ElementAt(1).Id : null;
                formation.Player_3Id = team.Squad.ElementAt(2) != null ? (int?)team.Squad.ElementAt(2).Id : null;
                formation.Player_4Id = team.Squad.ElementAt(3) != null ? (int?)team.Squad.ElementAt(3).Id : null;
                formation.Player_5Id = team.Squad.ElementAt(4) != null ? (int?)team.Squad.ElementAt(4).Id : null;
                formation.Player_6Id = team.Squad.ElementAt(5) != null ? (int?)team.Squad.ElementAt(5).Id : null;
                formation.Player_7Id = team.Squad.ElementAt(6) != null ? (int?)team.Squad.ElementAt(6).Id : null;
                formation.Player_8Id = team.Squad.ElementAt(7) != null ? (int?)team.Squad.ElementAt(7).Id : null;
                formation.Player_9Id = team.Squad.ElementAt(8) != null ? (int?)team.Squad.ElementAt(8).Id : null;
                formation.Player_10Id = team.Squad.ElementAt(9) != null ? (int?)team.Squad.ElementAt(9).Id : null;
                formation.Player_11Id = team.Squad.ElementAt(10) != null ? (int?)team.Squad.ElementAt(10).Id : null;

                SquadRepository.AddFormation(formation);
            }

            return formations;
        }
    }
}
