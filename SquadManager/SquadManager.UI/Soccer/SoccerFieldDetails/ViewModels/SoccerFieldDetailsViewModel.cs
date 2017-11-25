using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerFieldDetails.ViewModels
{
    public class SoccerFieldDetailsViewModel : ViewModel, IChangeable
    {
        public List<FormationViewModel> Formations { get; set; }

        public SoccerFieldDetailsViewModel()
        {
            Formations = new List<FormationViewModel>()
            {
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
                new FormationViewModel() { Name = "4-4-2" },
            };
        }

        public void Changed(ChangeArgs args)
        {

        }
    }
}
