using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewFamilyMemberGeneratorDefinition",
        menuName = "Game/Family Member Generator")]
    public sealed class FamilyMemberGeneratorDefinition : ScriptableObject
    {
        [SerializeField]
        private FamilyMemberCategoryDefinition[] categories;
        public FamilyMemberCategoryDefinition[] Categories
            => categories;
    }
}