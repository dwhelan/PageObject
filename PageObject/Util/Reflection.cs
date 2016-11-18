using System;
using System.Globalization;
using System.Reflection;

namespace PageObject.Util
{
    internal static class Reflection
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        internal static object CreateInstance(string typeName, params object[] parameters)
        {
            return CreateInstance(Type.GetType(typeName), parameters);
        }

        internal static object CreateInstance(Type type, params object[] parameters)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return Activator.CreateInstance(type, Flags, null, parameters, Culture);
        }

        internal static object Invoke(object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, Flags);
            return method.Invoke(obj, parameters);
        }
    }
}