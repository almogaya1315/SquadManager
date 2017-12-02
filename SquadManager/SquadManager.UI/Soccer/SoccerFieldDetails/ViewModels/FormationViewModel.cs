using SquadManager.UI.Base;
using SquadManager.UI.Extensions;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class FormationViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }

        private SquadList<SoccerPlayerViewModel> _lineup;
        public SquadList<SoccerPlayerViewModel> Lineup
        {
            get { return _lineup; }
            set
            {
                if (Equals(_lineup, value)) return;
                _lineup = value;
                RaisePropertyChanged();
            }
        }
    }
}
