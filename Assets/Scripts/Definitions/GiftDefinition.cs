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
        public string NameText
            => nameText;

        [SerializeField]
        private int cost;
        public int Cost
            => cost;

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite
            => sprite;
    }
}