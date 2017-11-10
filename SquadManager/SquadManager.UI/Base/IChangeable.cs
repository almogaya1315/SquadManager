using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public interface IChangeable
    {
        void Changed(ChangeArgs args);
    }
}
