using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewFamilyMemberCategoryDefinition",
        menuName = "Game/Family Member Category")]
    public sealed class FamilyMemberCategoryDefinition : ScriptableObject
    {
        [SerializeField]
        private string[] nameOptions;
        public string[] NameOptions
            => nameOptions;

        [SerializeField]
        private string[] relationshipNameOptions;
        public string[] RelationshipNameOptions
            => relationshipNameOptions;

        [SerializeField]
        private Sprite[] portraitOptions;
        public Sprite[] PortraitOptions
            => portraitOptions;

        [SerializeField]
        private GiftCategoryDefinition[] giftCategoryOptions;
        public GiftCategoryDefinition[] GiftCategoryOptions
            => giftCategoryOptions;

        [SerializeField]
        private QuirkDefinition[] quirkOptions;
        public QuirkDefinition[] QuirkOptions
            => quirkOptions;
    }
}