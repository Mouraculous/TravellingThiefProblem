using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravellingThief.Utilities
{
    public static class Extensions
    {
        public static T[] Slice<T>(this T[] source, int index, int length)
        {
            var slice = new T[length];
            Array.Copy(source, index, slice, 0, length);
            return slice;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
