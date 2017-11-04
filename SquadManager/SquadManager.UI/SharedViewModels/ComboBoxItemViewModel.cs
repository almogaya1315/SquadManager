using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class ComboBoxItemViewModel : ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ComboBoxItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
