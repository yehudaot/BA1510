using System;
using System.Collections.Generic;
using System.Linq;

namespace atpLib.Infra
{
    public static class ReflectiveEnumerator
    {
        static ReflectiveEnumerator() { }

        public static IEnumerable<T> GetEnumerableOfInterface<T>(params object[] constructorArgs) where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                //Assembly.GetCallingAssembly().GetTypes()
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.GetInterfaces().Contains(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            //objects.Sort();
            return objects;
        }

        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                //Assembly.GetCallingAssembly().GetTypes()
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            //objects.Sort();
            return objects;
        }
    }
}
