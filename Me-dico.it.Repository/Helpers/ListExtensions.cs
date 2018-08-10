using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Helpers
{
    public static class ListExtensions
    {
        public static void RemoveRange<T>(this List<T> source, IEnumerable<T> rangeToRemove)
        {
            if (rangeToRemove == null | !rangeToRemove.Any())
            {
                foreach (T item in rangeToRemove)
                {
                    source.Remove(item);
                }
            }
        }
    }
}
