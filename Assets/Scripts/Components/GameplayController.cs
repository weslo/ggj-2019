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
    {
        [SerializeField]
        private FamilyMemberGeneratorDefinition familyMemberGenerator = default(FamilyMemberGeneratorDefinition);

        [SerializeField]
        private int startingBudget = default(int);

        public event Action<FamilyMember, Gift> OnGiftSelected;

        public event Action<int> OnBudgetChanged;

        public ReadOnlyCollection<FamilyMember> FamilyMembers
        {
            get;
            private set;
        }

        private int _budget;
        public int Budget
        {
            get => _budget;
            private set
            {
                if(_budget != value)
                {
                    _budget = value;
                    OnBudgetChanged?.Invoke(_budget);
                }
            }
        }

        private Dictionary<FamilyMember, Gift> selectedGifts
            = new Dictionary<FamilyMember, Gift>();

        protected override void Awake()
        {
            base.Awake();
            RestartGame();
        }

        public void RestartGame()
        {
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

            Budget = startingBudget;
            
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

            if(gift.Cost > Budget)
            {
                throw new Exception($"Cannot afford {gift.Cost}.");
            }

            Budget -= gift.Cost;

            selectedGifts[member] = gift;
            OnGiftSelected?.Invoke(member, gift);

            if(IsGameEnded())
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

        public bool IsGameEnded()
        {
            bool purchasedGiftsForEveryone = selectedGifts.All(kvp => kvp.Value != null);
            bool cannotAffordAnyMoreGifts = selectedGifts
                .Keys
                .All(m => m
                    .GiftRequest
                    .GiftOptions
                    .Keys
                    .All(g => g.Cost > Budget));

            return purchasedGiftsForEveryone || cannotAffordAnyMoreGifts;
        }
    }
}