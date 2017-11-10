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
    }

    public class PlayerSelectedChangeArgs : ChangeArgs
    {
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
    }

    public enum ChangeType
    {
        PlayerSelected,
    }
}
