using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class ViewModel : ViewModelBase
    {
        public ViewModelManager Manager { get; set; }

        public ISquadRepository SquadRepository { get; set; }
    }
}
