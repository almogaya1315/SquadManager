using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Base
{
    public class ConstructorParameter
    {
        public string ParameterName { get; set; }

        public object ParameterValue { get; set; }

        public ConstructorParameter(string parameterName, object parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }
    }
}
