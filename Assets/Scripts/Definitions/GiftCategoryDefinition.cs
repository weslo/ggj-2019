using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewGiftCategoryDefinition",
        menuName = "Game/Gift Category")]
    public sealed class GiftCategoryDefinition : ScriptableObject
    {
        [SerializeField]
        private string descriptionText;
        public string DescriptionText
            => descriptionText;

        [SerializeField]
        private GiftDefinition[] giftOptions;
        public GiftDefinition[] GiftOptions
            => giftOptions;
    }
}