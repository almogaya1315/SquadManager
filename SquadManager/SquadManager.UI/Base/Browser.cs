using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using System;
using System.Collections.Generic;

namespace SquadManager.UI.Base
{
    public class Browser
    {
        private List<IBrowsable> _browsables;

        public Browser(ViewModelManager viewModelManager)
        {
            _browsables = new List<IBrowsable>()
            {
                viewModelManager.Menu, 
            };
        }

        public void Browse()
        {
            throw new NotImplementedException();
        }
    }
}
