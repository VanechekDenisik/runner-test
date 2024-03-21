using UnityEngine;

namespace Core.Helpers
{
    /// <summary>
    ///     Contains helpful methods to deal with operations with vectors
    /// </summary>
    public static class VectorsHelper
    {
        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static bool Has(this Vector2Int range, int value)
        {
            return range.x <= value && value <= range.y;
        }

        public static bool Has(this Vector2 range, float value)
        {
            return range.x <= value && value <= range.y;
        }
    }
}