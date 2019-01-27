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
                if(Application.platform == RuntimePlatform.OSXPlayer)
                {
                    PlayerPrefs.DeleteAll();
                    Screen.SetResolution(1024, 768, FullScreenMode.Windowed);
                }

                SceneFunctions.LoadScene("Persistent");
                
                initialized = true;
            }
        }
    }
}