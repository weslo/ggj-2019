using Game.Attributes;
using Game.Components.UI.Abstract;
using Game.UnityExtensions.Scenes;

namespace Game.Components.UI.StartScreen
{
    public sealed class StartScreenController : UIMonoBehaviour
    {
        [UnityEventBinding]
        public void OnPressPlay()
        {
            SceneFunctions.TransitionScene("StartScreen", "Gameplay");
        }
    }
}