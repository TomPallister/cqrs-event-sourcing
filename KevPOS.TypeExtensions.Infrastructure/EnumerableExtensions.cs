using System;
using System.Collections.Generic;
using System.Linq;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable)
                action.Invoke(element);
        }

        public static IEnumerable<IList<T>> Chunk<T>(this IList<T> source, int chunksize)
        {
            int pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize).ToList();
                pos += chunksize;
            }
        }
    }
}