using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.SharedViewModels
{
    public class ColumnViewModel : ViewModel
    {
        public string Header { get; set; }
        public string HeaderTemplate { get; set; }
        public string EditingTemplate { get; set; }
        public string Template { get; set; }
        public string DataContextPath { get; set; }
    }
}
