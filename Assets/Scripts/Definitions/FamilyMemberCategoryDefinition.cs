using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewFamilyMemberCategoryDefinition",
        menuName = "Game/Family Member Category")]
    public sealed class FamilyMemberCategoryDefinition : ScriptableObject
    {
        [SerializeField]
        private string[] nameOptions = default(string[]);
        public string[] NameOptions
            => nameOptions;

        [SerializeField]
        private string[] relationshipNameOptions = default(string[]);
        public string[] RelationshipNameOptions
            => relationshipNameOptions;

        [SerializeField]
        private Sprite[] portraitOptions = default(Sprite[]);
        public Sprite[] PortraitOptions
            => portraitOptions;

        [SerializeField]
        private GiftCategoryDefinition[] giftCategoryOptions = default(GiftCategoryDefinition[]);
        public GiftCategoryDefinition[] GiftCategoryOptions
            => giftCategoryOptions;

        [SerializeField]
        private QuirkDefinition[] quirkOptions = default(QuirkDefinition[]);
        public QuirkDefinition[] QuirkOptions
            => quirkOptions;
    }
}