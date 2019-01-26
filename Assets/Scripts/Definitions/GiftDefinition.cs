using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewGiftDefinition",
        menuName = "Game/Gift")]
    public sealed class GiftDefinition : ScriptableObject
    {
        [SerializeField]
        private string nameText = default(string);
        public string NameText
            => nameText;

        [SerializeField]
        private int cost = default(int);
        public int Cost
            => cost;

        [SerializeField]
        private Sprite sprite = default(Sprite);
        public Sprite Sprite
            => sprite;
    }
}