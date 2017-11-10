using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.LoadTeam.ViewModels
{
    public class LoadTeamViewModel : ViewModel
    {
        public List<TeamViewModel> Teams { get; set; }

        public LoadTeamViewModel() { }
        public LoadTeamViewModel(Application app, ISquadRepository squadRepository, CollectionFactory collection)
        {
            App = app;
            SquadRepository = squadRepository;
            Collections = collection;

            Teams = collection.TeamViewModels;
        }
    }
}
