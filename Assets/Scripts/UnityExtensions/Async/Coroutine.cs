using System.Collections;
using UnityEngine;

namespace Game.UnityExtensions.Async
{
    public class CoroutineHost : MonoBehaviour
    {
    }

    public static class Coroutine
    {
        private static CoroutineHost host;

        public static void Start(IEnumerator routine)
        {
            if(host == null)
            {
                host = new GameObject("Coroutine Host").AddComponent<CoroutineHost>();
                Object.DontDestroyOnLoad(host);
            }

            host.StartCoroutine(routine);
        }
    }
}