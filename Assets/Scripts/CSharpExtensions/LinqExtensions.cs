using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.CSharpExtensions
{
    public static class LinqExtensions
    {
        public static T PickRandom<T>(this IEnumerable<T> collection)
        {
            if(collection == null)
            {
                return default(T);
            }

            int count = collection.Count();

            if(count <= 0)
            {
                return default(T);
            }

            int index = UnityEngine.Random.Range(0, count);
            return collection.ElementAt(index);
        }

        public static IEnumerable<T> PickRandomUnique<T>(this IEnumerable<T> collection, int count)
        {
            List<T> list = collection.ToList();
            List<T> selected = new List<T>();

            Functions.Repeat(count, () =>
            {
                T element = list.PickRandom();
                selected.Add(element);
                list.Remove(element);
            });

            return selected;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if(action != null)
            {
                foreach(T element in collection)
                {
                    action(element);
                }
            }

            return collection;
        }

        public static IEnumerable<T> Map<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            if(action != null)
            {
                int count = collection.Count();
                for(int i = 0; i < count; i++)
                {
                    T element = collection.ElementAt(i);
                    action(element, i);
                }
            }

            return collection;
        }
    }
}