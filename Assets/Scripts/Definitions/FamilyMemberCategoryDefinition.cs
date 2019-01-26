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

        [SerializeField]
        private string[] relationshipNameOptions;

        [SerializeField]
        private Sprite[] portraitOptions;

        [SerializeField]
        private GiftCategoryDefinition[] giftCategoryOptions;

        [SerializeField]
        private QuirkDefinition[] quirkOptions;
    }
}