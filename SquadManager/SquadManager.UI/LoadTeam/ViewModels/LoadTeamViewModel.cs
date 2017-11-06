using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.LoadTeam.ViewModels
{
    public class LoadTeamViewModel : ViewModel
    {
        public LoadTeamViewModel() { }
        public LoadTeamViewModel(Application app, ISquadRepository squadRepository)
        {
            App = app;
            SquadRepository = squadRepository;
        }
    }
}
