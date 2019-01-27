using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Game.Attributes;
using Game.Components.Scheduling;
using Game.Components.UI.Abstract;
using Game.CSharpExtensions;
using Game.Gameplay;

namespace Game.Components.UI.GameplayScreen
{
    public sealed class FamilyMemberSelector : UIMonoBehaviour
    {
        [Serializable]
        public sealed class FamilyMemberEvent : UnityEvent<FamilyMember> { }

        [Header("Object References")]

        [SerializeField]
        private FamilyMemberPortrait portraitPrefab = default(FamilyMemberPortrait);

        [SerializeField]
        private RectTransform portraitContainer = default(RectTransform);

        [Header("Tranformations")]

        [SerializeField]
        private float distanceBetweenPortraits = default(float);

        [SerializeField]
        private float selectedPortraitScale = 1;

        [SerializeField]
        private float deselectedPortraitScale = 1;

        [Header("Events")]

        [SerializeField]
        private FamilyMemberEvent onSelectFamilyMember = default(FamilyMemberEvent);

        private int _selectedPortraitIndex = -1;
        public int SelectedPortraitIndex
        {
            get => _selectedPortraitIndex;
            private set
            {
                if(_selectedPortraitIndex != value)
                {
                    _selectedPortraitIndex = value;
                    onSelectFamilyMember?.Invoke(SelectedPortrait?.FamilyMember);

                    portraits.ForEach((portrait, index) =>
                    {
                        TransformTweens.TweenLocalPosition(portrait.RectTransform,
                            Vector3.right * distanceBetweenPortraits * (index - _selectedPortraitIndex),
                            TransformTweens.QuickTweenDuration);
                        TransformTweens.TweenLocalScale(portrait.RectTransform,
                            Vector3.one * (SelectedPortrait == portrait ? selectedPortraitScale : deselectedPortraitScale),
                            TransformTweens.QuickTweenDuration);
                    });
                }
            }
        }

        public FamilyMemberPortrait SelectedPortrait
        {
            get
            {
                return portraits.ElementAtOrDefault(SelectedPortraitIndex);
            }
        }

        private FamilyMemberPortrait[] portraits;

        void Start()
        {
            portraits = GameplayController
                .Instance
                .FamilyMembers
                .Select(member => 
                    Instantiate(
                        original: portraitPrefab,
                        parent: portraitContainer)
                        .SetFamilyMember(member))
                .ToArray();

            SelectedPortraitIndex = 0;

            GameplayController.Instance.OnGiftSelected += OnGiftSelected;
        }

        void OnDestroy()
        {
            GameplayController.Instance.OnGiftSelected -= OnGiftSelected;
            TimerManager.Cancel(this);
        }

        [UnityEventBinding]
        public void SelectRight()
        {
            if(SelectedPortraitIndex + 1 >= portraits.Length)
            {
                return;
            }
            
            SelectedPortraitIndex++;
        }

        [UnityEventBinding]
        public void SelectLeft()
        {
            if(SelectedPortraitIndex <= 0)
            {
                return;
            }

            SelectedPortraitIndex--;
        }

        private void OnGiftSelected(FamilyMember member, Gift gift)
        {
            if(SelectedPortrait.FamilyMember == member)
            {
                TimerManager.Schedule(
                    time: TransformTweens.QuickTweenDuration,
                    id: this)
                    .OnComplete(SelectNextAvailable);
            }
        }

        private void SelectNextAvailable()
        {
            if(GameplayController.Instance.IsGameEnded())
            {
                return;
            }

            Func<int, bool> giftNotPurchased = i
                => GameplayController.Instance.GetSelectedGift(portraits[i].FamilyMember) == null;

            for(int i = SelectedPortraitIndex + 1; i < portraits.Length; i++)
            {
                if(giftNotPurchased(i))
                {
                    SelectedPortraitIndex = i;
                    return;
                }
            }

            for(int i = 0; i < SelectedPortraitIndex; i++)
            {
                if(giftNotPurchased(i))
                {
                    SelectedPortraitIndex = i;
                    return;
                }
            }
        }
    }
}