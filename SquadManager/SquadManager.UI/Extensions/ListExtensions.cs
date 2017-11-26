using GalaSoft.MvvmLight;
using SquadManager.UI.Extensions;
using SquadManager.UI.Soccer.SoccerPlayerDetails.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SquadManager.UI.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> list)
        {
            var observables = new ObservableCollection<T>();

            foreach (var item in list) observables.Add(item);

            return observables;
        }

        public static SquadList<SoccerPlayerViewModel> ToSquadList<T>(this IEnumerable<T> list) where T : SoccerPlayerViewModel
        {
            var squadList = new SquadList<SoccerPlayerViewModel>();
            foreach (var player in list) squadList.Add(player);
            return squadList;
        }
    }
}
