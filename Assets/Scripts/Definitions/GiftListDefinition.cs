using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "New Gift List Definition",
        menuName = "Game/GiftListDefinition")]
    public sealed class GiftListDefinition : ScriptableObject
    {
        [SerializeField]
        private string descriptionText;

        [SerializeField]
        private GiftDefinition[] giftOptions;
    }
}