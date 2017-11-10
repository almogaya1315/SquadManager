using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerSquadDetails.ViewModels
{
    public class SoccerSquadDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;

        public SoccerSquadDetailsViewModel(IChangeManager changesManager)
        {
            _changesManager = changesManager;
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
