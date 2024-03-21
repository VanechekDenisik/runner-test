using UnityEngine;

namespace Core.Helpers
{
    public static class ColorsHelper
    {
        public static string Hex(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}