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
    }
}