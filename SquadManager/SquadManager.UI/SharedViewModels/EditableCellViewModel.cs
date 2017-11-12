using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class EditableCellViewModel : CellViewModel
    {
        public bool IsEnabled { get; set; }

        public EditableCellViewModel(object value, bool isEnabled = true) : base(value)
        {
            IsEnabled = isEnabled;
        }
    }
}
