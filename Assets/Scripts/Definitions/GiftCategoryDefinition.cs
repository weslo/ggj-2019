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

        [SerializeField]
        private GiftDefinition[] giftOptions;
    }
}