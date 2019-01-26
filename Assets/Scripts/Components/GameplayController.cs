using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Game.Components.Utility;
using Game.Definitions;
using Game.Gameplay;

namespace Game.Components
{
    public sealed class GameplayController : SingletonMonoBehaviour<GameplayController>
    {
        [Serializable]
        public sealed class GiftSelectionEvent : UnityEvent<FamilyMember, Gift> { }

        [Header("Object References")]

        [SerializeField]
        private FamilyMemberGeneratorDefinition familyMemberGenerator = default(FamilyMemberGeneratorDefinition);

        [Header("Events")]

        [SerializeField]
        private GiftSelectionEvent onGiftSelected = default(GiftSelectionEvent);

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
                .GenerateFamilyMembers(familyMemberGenerator, 5);
            
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

            if(!member.GiftRequest.GiftOptions.Contains(gift))
            {
                throw new Exception($"Gift {gift.Name} is not a valid choice for {member.Name}.");
            }

            selectedGifts[member] = gift;

            onGiftSelected?.Invoke(member, gift);
        }

        public Gift GetSelectedGift(FamilyMember member)
        {
            if(!selectedGifts.ContainsKey(member))
            {
                throw new Exception($"Selection dict does not contain {member.Name}.");
            }
            
            return selectedGifts[member];
        }
    }
}