using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay;

namespace Game.Components.UI
{
    public sealed class FamilyMemberPortrait : UIMonoBehaviour
    {
        [SerializeField]
        private Image portraitImage;

        private FamilyMember _familyMember;
        public FamilyMember FamilyMember
        {
            get => _familyMember;
            set
            {
                if(_familyMember != value)
                {
                    _familyMember = value;

                    if(portraitImage)
                    {
                        portraitImage.sprite = _familyMember?.PortraitSprite;
                    }
                }
            }
        }

        public FamilyMemberPortrait SetFamilyMember(FamilyMember member)
        {
            FamilyMember = member;
            return this;
        }
    }
}