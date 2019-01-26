using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "New Family Member Generator Definition",
        menuName = "Game/FamilyMemberGeneratorDefinition")]
    public sealed class FamilyMemberGeneratorDefinition : ScriptableObject
    {
        [SerializeField]
        private FamilyMemberCategoryDefinition[] categories;
    }
}