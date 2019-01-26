using System;

namespace Game.CSharpExtensions
{
    public static class Functions
    {
        public static void Repeat(int count, Action action)
        {
            if(action == null)
            {
                return;
            }

            for(int i = 0; i < count; i++)
            {
                action();
            }
        }

        public static void Repeat(int count, Action<int> action)
        {
            if(action == null)
            {
                return;
            }

            for(int i = 0; i < count; i++)
            {
                action(i);
            }
        }
    }
}