using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Arctic.Threading.Extensions
{
    public static class ListExtension
    {

        public static void WaitAll(this IList<Semaphore> resources)
        {
            if (!resources.Any())
            {
                return;
            }
            foreach (var resource in resources)
            {
                resource.WaitOne();
            }
        }

        public static void ReleaseAll(this IList<Semaphore> resources)
        {
            if (!resources.Any())
            {
                return;
            }
            foreach (var resource in resources)
            {
                resource.Release();
            }
        }

        public static int FindIndex<T>(this IList<T> source, int startIndex, Predicate<T> match)
        {
            for (int i = startIndex; i < source.Count; i++)
            {
                if (match(source[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
