using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewGiftDefinition",
        menuName = "Game/Gift")]
    public sealed class GiftDefinition : ScriptableObject
    {
        [SerializeField]
        private string nameText;

        [SerializeField]
        private int cost;

        [SerializeField]
        private Sprite sprite;
    }
}