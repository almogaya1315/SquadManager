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

    public class SoccerPlayerArgs : ChangeArgs
    {
        public SoccerPlayer NewPlayerValues { get; set; }

        //public SoccerPlayer OriginalBallValues { get; set; }

        public SoccerPlayerArgs(SoccerPlayer newPlayerValues, ChangeType type, SoccerPlayer originalPlayerValues = null) : base(type)
        {
            NewPlayerValues = newPlayerValues;
            //if (originalPlayerValues != null) OriginalBallValues = originalPlayerValues;
        } 
    }

    public enum ChangeType
    {
        TeamChanged,
        PlayerAdded,
        PlayerChanged,
    }
}
