using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Helpers
{
    /// <summary>
    ///     Contains helpful methods to deal with random operations
    /// </summary>
    public static class RandomHelper
    {
        /// <summary> Returns random element from the array </summary>
        public static T Random<T>(this T[] elements)
        {
            return elements[UnityEngine.Random.Range(0, elements.Length)];
        }

        /// <summary> Returns random element from the list </summary>
        public static T Random<T>(this IReadOnlyList<T> elements)
        {
            return elements[UnityEngine.Random.Range(0, elements.Count)];
        }

        /// <summary> Returns random element from the array with given weights</summary>
        public static T Random<T>(T[] array, float[] weights)
        {
            if (array.Length == 0) return default;

            if (array.Length != weights.Length)
            {
                Debug.LogErrorFormat(
                    "Utils:random(list, weights):list.count != weights.count!!! list.count = {0}, weights.count = {1}",
                    array.Length, weights.Length);
                return default;
            }

            var sum = weights.Sum();
            float curr = 0;
            var random = UnityEngine.Random.Range(0, sum);
            for (var i = 0; i < array.Length; i++)
            {
                curr += weights[i];
                if (random <= curr)
                    return array[i];
            }

            return default;
        }

        /// <summary> Returns random int value from 0 (inclusive) to count (exclusive) </summary>
        public static int Random(int count)
        {
            return UnityEngine.Random.Range(0, count);
        }

        /// <summary> Returns random int value from min (inclusive) to max (exclusive) </summary>
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary> Returns random float value from min to max </summary>
        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary> Returns random int value from min (inclusive) to max (exclusive) </summary>
        public static int Random(this Vector2Int range)
        {
            return Random(range.x, range.y);
        }

        /// <summary> Returns random float value from min to max </summary>
        public static float Random(this Vector2 range)
        {
            return Random(range.x, range.y);
        }

        /// <summary> Returns true if generated random number is less then chance, otherwise false. Chance = [0, 1] </summary>
        public static bool Random(float chance)
        {
            return UnityEngine.Random.Range(0f, 1f) <= chance;
        }

        /// <summary> Returns random int number from 0 (inclusive) to weights.Lenght (exclusive) with given weights</summary>
        public static int Random(params float[] weights)
        {
            if (weights.Length == 0)
                return -1;

            var sum = weights.Sum();
            float curr = 0;
            var random = UnityEngine.Random.Range(0, sum);
            for (var i = 0; i < weights.Length; i++)
            {
                curr += weights[i];
                if (random <= curr)
                    return i;
            }

            return -1;
        }

        /// <summary> Returns random float number from 0 to 1</summary>
        public static float Random()
        {
            return Random(0f, 1f);
        }
    }
}