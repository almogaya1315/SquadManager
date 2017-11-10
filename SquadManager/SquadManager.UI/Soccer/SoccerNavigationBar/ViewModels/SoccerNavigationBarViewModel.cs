﻿using SquadManager.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerNavigationBar.ViewModels
{
    public class SoccerNavigationBarViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;

        public SoccerNavigationBarViewModel(IChangeManager changesManager)
        {
            _changesManager = changesManager;
        }

        public void Changed(ChangeArgs args)
        {
         
        }
    }
}