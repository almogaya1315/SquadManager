using SquadManager.UI.Base;
using SquadManager.UI.Models;
using SquadManager.UI.TeamDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels
{
    public class SoccerPlayerDetailsViewModel : ViewModel, IChangeable
    {
        private readonly IChangeManager _changesManager;
        private SoccerPlayer _playerModel;

        private SoccerPlayerViewModel _player;
        public SoccerPlayerViewModel Player
        {
            get { return _player; }
            set
            {
                _player = value;
                RaisePropertyChanged();
            }
        }

        public SoccerPlayerDetailsViewModel(SoccerPlayer player, CollectionFactory collections, IChangeManager changesManager)
        {
            Collections = collections;

            _changesManager = changesManager;
            _playerModel = player;

            Player = new SoccerPlayerViewModel(player);
        }

        public void Changed(ChangeArgs args)
        {
            if (args.Type == ChangeType.PlayerSelected || args.Type == ChangeType.PlayerChanged)
            {
                var playerArgs = (SoccerPlayerArgs)args;
                Player = new SoccerPlayerViewModel(playerArgs.PlayerValues, Collections, _changesManager);
            }
        }
    }
}
