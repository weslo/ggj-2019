using Game.Attributes;
using Game.Components.UI.Abstract;
using Game.UnityExtensions.Scenes;

namespace Game.Components.UI.StartScreen
{
    public sealed class AboutScreenController : UIMonoBehaviour
    {
        [UnityEventBinding]
        public void OnPressBackButton()
        {
            SceneFunctions.TransitionScene("AboutScreen", "StartScreen");
        }
    }
}