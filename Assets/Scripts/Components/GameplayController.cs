using UnityEngine;
using Game.Components.Utility;
using Game.CSharpExtensions;
using Game.Definitions;
using Game.Gameplay;

namespace Game.Components
{
    public sealed class GameplayController : SingletonMonoBehaviour<GameplayController>
    {
        [SerializeField]
        private FamilyMemberGeneratorDefinition familyMemberGenerator;

        void Start()
        {
            FamilyMemberGeneration
                .GenerateFamilyMembers(familyMemberGenerator, 5)
                .ForEach(member =>
                {
                    Debug.Log($"{member.Name} ({member.RelationshipName}) wants {member.GiftRequest.DescriptionText}.");
                });
        }
    }
}