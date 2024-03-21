using System.Collections.Generic;

namespace Core.Helpers
{
    public static class StringsHelper
    {
        public static bool IsIdEqualsTo(this string id1, string id2)
        {
            return id1.Trim().ToLower() == id2.Trim().ToLower();
        }

        public static string MergeStrings(this IEnumerable<string> array, string separator)
        {
            if (array == null) return "";
            
            var result = "";
            foreach (var element in array)
            {
                if (element.IsEmpty()) continue;
                result += separator + element;
            }
            
            if (!result.IsEmpty())
                result = result.Remove(0, separator.Length);
            
            return result;
        }
        
        public static string SafeReplace(this string str, string oldValue, string newValue)
        {
            if (str.IsEmpty()) return str;
            if (!str.Contains(oldValue)) return str;
            return str.Replace(oldValue, newValue);
        }
    }
}