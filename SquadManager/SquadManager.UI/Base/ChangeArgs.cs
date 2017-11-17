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

    public class TeamChangedArgs : ChangeArgs
    {
        //public PropertyName Property { get; set; }

        public TeamChangedArgs(ChangeType type) : base(type) { } 
    }

    public enum ChangeType
    {
        TeamChanged,
        PlayerAdded,
    }
}
