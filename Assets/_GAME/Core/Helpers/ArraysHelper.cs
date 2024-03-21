using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    /// <summary>
    ///     Contains helpful methods to deal with arrays operations.
    ///     You can work with arrays without checking an array for null or converting to List or Array.
    /// </summary>
    public static class ArraysHelper
    {
        public static bool IsEmpty<T>(this IEnumerable<T> array)
        {
            return array == null || !array.Any();
        }

        public static bool IsEmpty<T1, T2>(this Dictionary<T1, T2> dict)
        {
            return dict == null || dict.Count == 0;
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool Has<T>(this T[] array, T element)
        {
            return array != null && array.Contains(element);
        }

        public static int Size<T>(this T[] array)
        {
            return array.IsEmpty() ? 0 : array.Length;
        }

        public static int Size<T>(this List<T> array)
        {
            return array.IsEmpty() ? 0 : array.Count;
        }

        public static T Get<T>(this T[] array, Func<T, bool> match)
        {
            return array.IsEmpty() ? default : array.FirstOrDefault(match);
        }

        public static T Get<T>(this List<T> array, Func<T, bool> match)
        {
            return array.IsEmpty() ? default : array.FirstOrDefault(match);
        }

        public static T2 Get<T1, T2>(this Dictionary<T1, T2> dict, T1 key)
        {
            return dict.IsEmpty() ? default : dict.ContainsKey(key) ? dict[key] : default;
        }
        
        public static void AddOrSum<TKey>(this Dictionary<TKey, int> dict, TKey key, int value)
        {
            if (!dict.TryAdd(key, value))
                dict[key] += value;
        }
        
        public static void AddOrSum<TKey>(this Dictionary<TKey, float> dict, TKey key, float value)
        {
            if (!dict.TryAdd(key, value))
                dict[key] += value;
        }

        public static T Next<T>(this T[] array, T element)
        {
            return array.ToList().Next(element);
        }

        public static T Next<T>(this List<T> array, T element)
        {
            var elementIndex = array.IndexOf(element);
            if (elementIndex == -1 ||
                elementIndex + 1 >= array.Count)
                return default;

            return array[elementIndex + 1];
        }

        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var element in array)
                action?.Invoke(element);
        }
        
        public static IEnumerable<T> With<T>(this IEnumerable<T> array, IEnumerable<T> elements)
        {
            var temp = new List<T>();
            if (!array.IsEmpty())
                temp.AddRange(array);
            if (!elements.IsEmpty())
                temp.AddRange(elements);
            return temp.ToArray();
        }
        
        public static IEnumerable<T> With<T>(this IEnumerable<T> array, T element)
        {
            return array.With(new[] { element });
        }
        
        public static IEnumerable<T> Without<T>(this IEnumerable<T> array, IEnumerable<T> elements)
        {
            if (array.IsEmpty())
                return array;
            var temp = array.ToList();
            foreach (var element in elements)
                temp.Remove(element);
            return temp.ToArray();
        }
        
        public static IEnumerable<T> Without<T>(this IEnumerable<T> array, T element)
        {
            return array.Without(new[] { element });
        }

        public static void SortBy<TSource>(this List<TSource> array, Func<TSource, float> selector)
        {
            if (array.Size() <= 1)
                return;

            array.Sort((i1, i2) => selector(i1) > selector(i2) ? 1 : selector(i1) < selector(i2) ? -1 : 0);
        }

        public static void RemoveRange<T>(this List<T> array, T[] toRemove)
        {
            foreach (var t in toRemove)
                array.Remove(t);
        }

        public static List<T> Reversed<T>(this List<T> array)
        {
            var result = new List<T>();
            for (var id = array.Count - 1; id >= 0; id--)
                result.Add(array[id]);

            return result;
        }

        public static T[] Reversed<T>(this T[] array)
        {
            var result = new List<T>();
            for (var id = array.Length - 1; id >= 0; id--)
                result.Add(array[id]);

            return result.ToArray();
        }

        public static T[] Shuffled<T>(this IEnumerable<T> array)
        {
            return array.OrderBy(_ => RandomHelper.Random()).ToArray();
        }

        public static bool HasIdenticalElements<T>(this IReadOnlyList<T> list1, IReadOnlyList<T> list2)
        {
            if (list1.Count != list2.Count) return false;

            foreach (var element in list1)
                if (!list2.Contains(element))
                    return false;

            return true;
        }
    }
}