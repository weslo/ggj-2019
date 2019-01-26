using UnityEngine;
using Game.UnityExtensions.Scenes;

namespace Game.Components.Utility
{
    public sealed class GameInitializer : MonoBehaviour
    {
        private static bool initialized = false;

        void Awake()
        {
            if(!initialized)
            {
                SceneFunctions.LoadScene("Persistent");
                initialized = true;
            }
        }
    }
}