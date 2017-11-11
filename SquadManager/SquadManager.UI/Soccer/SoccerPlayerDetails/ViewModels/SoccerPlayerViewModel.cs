using SquadManager.UI.Base;
using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels
{
    public class SoccerPlayerViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }

        public SoccerPlayerViewModel(SoccerPlayer model)
        {

        }
    }
}
