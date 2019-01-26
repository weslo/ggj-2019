using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "New Gift Definition",
        menuName = "Game/GiftDefinition")]
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