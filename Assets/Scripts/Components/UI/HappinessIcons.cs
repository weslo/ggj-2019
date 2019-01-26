using UnityEngine;
using Game.Components.Utility;
using Game.Gameplay;

namespace Game.Components.UI
{
    public sealed class HappinessIcons : PersistentSingletonMonoBehaviour<HappinessIcons>
    {
        [SerializeField]
        private Sprite invalidSprite = default(Sprite);

        [SerializeField]
        private Sprite satisfiedSprite = default(Sprite);

        [SerializeField]
        private Sprite happySprite = default(Sprite);

        [SerializeField]
        private Sprite loveSprite = default(Sprite);

        public Sprite GetHappinessLevelSprite(HappinessLevel happinessLevel)
        {
            switch(happinessLevel)
            {
                case HappinessLevel.Satisfied: return satisfiedSprite;
                case HappinessLevel.Happy: return happySprite;
                case HappinessLevel.Love: return loveSprite;
                default: return invalidSprite;
            }
        }
    }
}