using System.Collections.ObjectModel;
using UnityEngine;
using Game.Components.Utility;
using Game.Definitions;
using Game.Gameplay;

namespace Game.Components
{
    public sealed class GameplayController : SingletonMonoBehaviour<GameplayController>
    {
        [SerializeField]
        private FamilyMemberGeneratorDefinition familyMemberGenerator;

        public ReadOnlyCollection<FamilyMember> FamilyMembers
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();

            FamilyMembers = FamilyMemberGeneration
                .GenerateFamilyMembers(familyMemberGenerator, 5);
        }
    }
}