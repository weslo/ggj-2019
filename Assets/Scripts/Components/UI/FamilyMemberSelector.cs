using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Game.Attributes;
using Game.CSharpExtensions;
using Game.Gameplay;

namespace Game.Components.UI
{
    public sealed class FamilyMemberSelector : UIMonoBehaviour
    {
        [Serializable]
        public sealed class FamilyMemberEvent : UnityEvent<FamilyMember> { }

        [Header("Object References")]

        [SerializeField]
        private FamilyMemberPortrait portraitPrefab;

        [SerializeField]
        private RectTransform portraitContainer;

        [Header("Tranformations")]

        [SerializeField]
        private float distanceBetweenPortraits;

        [SerializeField]
        private float selectedPortraitScale;

        [SerializeField]
        private float deselectedPortraitScale;

        [Header("Events")]

        [SerializeField]
        private FamilyMemberEvent onSelectFamilyMember;

        private int _selectedPortraitIndex = -1;
        public int SelectedPortraitIndex
        {
            get => _selectedPortraitIndex;
            private set
            {
                if(_selectedPortraitIndex != value)
                {
                    if(SelectedPortrait != null)
                    {
                        SelectedPortrait.RectTransform.localScale = Vector3.one * deselectedPortraitScale;
                    }

                    _selectedPortraitIndex = value;

                    if(SelectedPortrait != null)
                    {
                        SelectedPortrait.RectTransform.localScale = Vector3.one * selectedPortraitScale;
                    }

                    onSelectFamilyMember?.Invoke(SelectedPortrait?.FamilyMember);

                    portraits.Map((portrait, index) =>
                    {
                        portrait.RectTransform.localPosition = Vector3.right * distanceBetweenPortraits * (index - _selectedPortraitIndex);
                        portrait.RectTransform.localScale = Vector3.one * (SelectedPortrait == portrait ? selectedPortraitScale : deselectedPortraitScale);
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
    }
}