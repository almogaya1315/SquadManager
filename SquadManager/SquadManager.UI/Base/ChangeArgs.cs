using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class ChangeArgs
    {
        public ChangeType Type { get; set; }

        public ChangeArgs(ChangeType type)
        {
            Type = type;
        }
    }

    public class SoccerPlayerAddedArgs : ChangeArgs
    {
        public SoccerPlayer Player { get; set; }

        public SoccerPlayerAddedArgs(SoccerPlayer player, ChangeType type) : base(type)
        {
            Player = player;
        } 
    }

    public enum ChangeType
    {
        TeamChanged,
        PlayerAdded,
    }
}
