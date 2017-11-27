using SquadManager.UI.Enums;
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
        public SoccerPlayer PlayerValues { get; set; }

        public ColumnName Column { get; set; }

        //public SoccerPlayer OriginalBallValues { get; set; }

        public SoccerPlayerArgs(SoccerPlayer playerValues, ChangeType type, ColumnName? column = null, SoccerPlayer originalPlayerValues = null) : base(type)
        {
            PlayerValues = playerValues;
            if (column.HasValue) Column = column.Value;

            //if (originalPlayerValues != null) OriginalBallValues = originalPlayerValues;
        } 
    }

    public class SubstitutionArgs : ChangeArgs
    {
        public SoccerPlayer FirstSub { get; set; }
        public SoccerPlayer SecondSub { get; set; }

        public SubstitutionArgs(ChangeType type, SoccerPlayer firstSub, SoccerPlayer secondSub = null) : base (type)
        {
            FirstSub = firstSub;
            SecondSub = secondSub;
        }
    }

    public enum ChangeType
    {
        TeamChanged,
        PlayerAdded,
        PlayerChanged,
        PlayerDeleted,
        PlayerSelected,
        SubSelected,
        SubDeselect,
        SubConfirmed,
    }
}
