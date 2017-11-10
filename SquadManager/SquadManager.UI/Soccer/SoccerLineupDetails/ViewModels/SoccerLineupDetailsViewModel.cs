using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerLineupDetails.ViewModels
{
    public class SoccerLineupDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;

        public SoccerLineupDetailsViewModel(IChangeManager changesManager)
        {
            _changesManager = changesManager;
        }

        public void Changed(ChangeArgs args)
        {
            
        }
    }
}
