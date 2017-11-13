using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class ComboBoxViewModel : EditableCellViewModel
    {
        public List<ComboBoxItemViewModel> Items { get; set; }

        public ComboBoxViewModel(object value, List<ComboBoxItemViewModel> items, bool isEnabled = true) 
            : base(value, isEnabled)
        {
            Items = items;
        }
    }
}
