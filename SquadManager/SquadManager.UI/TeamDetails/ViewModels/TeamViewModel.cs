using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.TeamDetails.ViewModels
{
    public class TeamViewModel : ViewModel
    {
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public int NationId { get; set; }
        public int CityId { get; set; }
        public int Sport { get; set; }
    }
}
