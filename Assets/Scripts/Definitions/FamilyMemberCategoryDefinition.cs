using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "New Family Member Category Definition",
        menuName = "Game/FamilyMemberCategoryDefinition")]
    public sealed class FamilyMemberCategoryDefinition : ScriptableObject
    {
        [SerializeField]
        private string[] nameOptions;

        [SerializeField]
        private string[] relationshipNameOptions;

        [SerializeField]
        private Sprite[] portraitOptions;
    }
}