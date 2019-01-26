using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Game.Components.Utility;
using Game.Definitions;
using Game.Gameplay;
using Game.UnityExtensions.Scenes;

namespace Game.Components
{
    public sealed class GameplayController : PersistentSingletonMonoBehaviour<GameplayController>
    {[Header("Object References")]

        [SerializeField]
        private FamilyMemberGeneratorDefinition familyMemberGenerator = default(FamilyMemberGeneratorDefinition);

        public event Action<FamilyMember, Gift> OnGiftSelected;

        public ReadOnlyCollection<FamilyMember> FamilyMembers
        {
            get;
            private set;
        }

        private Dictionary<FamilyMember, Gift> selectedGifts;

        protected override void Awake()
        {
            base.Awake();

            FamilyMembers = FamilyMemberGeneration
                .GenerateFamilyMembers(
                    generator: familyMemberGenerator,
                    numFamilyMembers: 5,
                    giftHappinessOptions:new []
                        {
                            HappinessLevel.Satisfied,
                            HappinessLevel.Happy,
                            HappinessLevel.Love,
                        });
            
            selectedGifts = FamilyMembers
                .ToDictionary(
                    member => member,
                    member => null as Gift);
        }

        public void SetSelectedGift(FamilyMember member, Gift gift)
        {
            if(!selectedGifts.ContainsKey(member))
            {
                throw new Exception($"Selection dict does not contain {member.Name}.");
            }

            if(selectedGifts[member] != null)
            {
                throw new Exception($"{member.Name} already has a gift selected.");
            }

            if(!member.GiftRequest.GiftOptions.ContainsKey(gift))
            {
                throw new Exception($"Gift {gift.Name} is not a valid choice for {member.Name}.");
            }

            selectedGifts[member] = gift;
            OnGiftSelected?.Invoke(member, gift);

            bool gameEnded = selectedGifts.All(kvp => kvp.Value != null);
            if(gameEnded)
            {
                SceneFunctions.TransitionScene("Gameplay", "EndOfGameResults");
            }
        }

        public Gift GetSelectedGift(FamilyMember member)
        {
            if(!selectedGifts.ContainsKey(member))
            {
                throw new Exception($"Selection dict does not contain {member.Name}.");
            }
            
            return selectedGifts[member];
        }

        public ReadOnlyDictionary<FamilyMember, Gift> GetSelectedGifts()
        {
            return new ReadOnlyDictionary<FamilyMember, Gift>(selectedGifts);
        }
    }
}