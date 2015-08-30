using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            foreach (var item in enumerable)
            {
                action?.Invoke(item);
            }

            return enumerable;
        }

        public static ICollection<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (range == null) throw new ArgumentNullException(nameof(range));

            foreach (var item in range.ToArray())
            {
                collection.Remove(item);
            }

            return collection;
        }
    }
}