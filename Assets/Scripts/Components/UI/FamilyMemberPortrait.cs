using UnityEngine;
using UnityEngine.UI;
using Game.Components.UI.Abstract;
using Game.Gameplay;

namespace Game.Components.UI
{
    public sealed class FamilyMemberPortrait : UIMonoBehaviour
    {
        [SerializeField]
        private Image portraitImage = default(Image);

        [SerializeField]
        private Image happinessImage = default(Image);

        private FamilyMember _familyMember;
        public FamilyMember FamilyMember
        {
            get => _familyMember;
            set
            {
                if(_familyMember != value)
                {
                    _familyMember = value;

                    if(portraitImage != null)
                    {
                        portraitImage.sprite = _familyMember?.PortraitSprite;
                    }
                }
            }
        }

        private HappinessLevel _happinessLevel;
        public HappinessLevel HappinessLevel
        {
            get => _happinessLevel;
            set
            {
                if(_happinessLevel != value)
                {
                    _happinessLevel = value;
                    
                    if(happinessImage != null)
                    {
                        happinessImage.enabled = true;
                        happinessImage.sprite = HappinessIcons.Instance.GetHappinessLevelSprite(_happinessLevel);
                    }
                }
            }
        }

        public FamilyMemberPortrait SetFamilyMember(FamilyMember member)
        {
            FamilyMember = member;
            return this;
        }

        public FamilyMemberPortrait SetHappinessLevel(HappinessLevel happinessLevel)
        {
            HappinessLevel = happinessLevel;
            return this;
        }

        public void PlayEnterAnimation()
        {
            Animator.Play("Enter");
        }

        public void PlayPositivePingAnimation()
        {
            Animator.Play("PositivePing");
        }

        public void PlayNegativePingAnimation()
        {
            Animator.Play("NegativePing");
        }

        public void PlayQuirkPingAnimation()
        {
            Animator.Play("QuirkPing");
        }
    }
}