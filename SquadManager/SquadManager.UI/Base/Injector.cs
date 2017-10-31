using System.Collections.Generic;

namespace SquadManager.UI.Base
{
    public class Injector 
    {
        private List<ConstructorParameter> _parameters;

        public Injector()
        {
            _parameters = new List<ConstructorParameter>()
            {
                new Browser()
            };
        }

        public T New<T>(params ConstructorParameter[] parameters) where T : class, new() 
        {
            var parameterName = (parameters.GetValue(1) as ConstructorParameter).ParameterName;
            return new T();
        }
    }
}
