using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class CellViewModel : ViewModel
    {
        public object Value { get; set; }

        public CellViewModel(object value)
        {
            Value = value;
        }
    }
}
