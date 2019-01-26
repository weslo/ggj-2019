namespace Game.CSharpExtensions
{
    public static class ArrayExtensions
    {
        public static T PickRandom<T>(this T[] arr)
        {
            if(arr == null || arr.Length <= 0)
            {
                return default(T);
            }

            int index = UnityEngine.Random.Range(0, arr.Length);
            return arr[index];
        }

        public static int IndexOf<T>(this T[] arr, T element)
            where T : class
        {
            int found = -1;
            Functions.Repeat(arr.Length, i =>
            {
                if(arr[i] == element)
                {
                    found = i;
                    return;
                }
            });

            return found;
        }
    }
}