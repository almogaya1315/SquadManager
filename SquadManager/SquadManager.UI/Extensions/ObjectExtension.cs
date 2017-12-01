using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Extensions
{
    public static class ObjectExtension
    {
        public static T RefferenceCopy<T>(this T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var copy = JsonConvert.DeserializeObject<T>(json);

            return copy;
        }
    }
}
