using GalaSoft.MvvmLight;
using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Menu.ViewModels;
using SquadManager.UI.SquadDetails.ViewModels;
using System;

namespace SquadManager.UI.Base
{
    public class Injector
    {
        //SimpleInjector.Container _injectorContainer;

        private ViewModelManager _manager;

        public Injector()
        {
            /*
            _injectorContainer = new SimpleInjector.Container();
            _injectorContainer.Register<ViewModelManager>();
            _injectorContainer.Register<MenuViewModel>();
            _injectorContainer.Register<SquadDetailsViewModel>();
            */
        }

        public ViewModelManager GetManager()
        {
            return _manager = new ViewModelManager(this);
        }

        public T New<T>() where T : IViewModel, new()
        {
            var instance = new T();
            instance.Manager = _manager;
            return instance;
        }
    }
}
