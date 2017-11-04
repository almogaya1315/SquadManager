using SquadManager.UI.AppContainer.ViewModels;
using SquadManager.UI.Container.ViewModels;
using SquadManager.UI.Repositories;

namespace SquadManager.UI.Base
{
    public class Injector
    {
        private ViewModelManager _manager;

        private ISquadRepository _squadRepository;

        private CollectionFactory _collections;

        public Injector()
        {
            _squadRepository = new SquadRepository();
            _collections = new CollectionFactory(_squadRepository);
        }

        public ViewModelManager GetManager(ContainerViewModel container)
        {
            return _manager = new ViewModelManager(this, container);
        }

        public T New<T>() where T : ViewModel, new()
        {
            var instance = new T();
            //instance.Manager = _manager;
            instance.SquadRepository = _squadRepository;
            instance.Collections = _collections;
            return instance;
        }
    }
}
