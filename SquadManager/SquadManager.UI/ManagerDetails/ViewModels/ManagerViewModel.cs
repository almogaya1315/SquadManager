using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.ManagerDetails.ViewModels
{
    public class ManagerViewModel : ViewModel
    {
        public string Name { get; set; }
        public ComboBoxItemViewModel Nationality { get; set; }
        public int? Age { get; set; }
    }
}
