using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lab.Utility.MyCsharp
{
    public static class EnumHelper<T> where T : struct, Enum
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(fi => (T)Enum.Parse(value.GetType(), fi.Name, false))
                .ToArray();
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string[] GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToArray();
        }
    }
}
