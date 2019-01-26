using UnityEngine;
using UnityEngine.UI;
using Game.Attributes;
using Game.Gameplay;

namespace Game.Components.UI
{
    public sealed class FamilyMemberInfoDisplay : UIMonoBehaviour
    {
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text relationshipText;

        [SerializeField]
        private Text wantsText;

        [SerializeField]
        private Text quirkTitleText;

        [SerializeField]
        private Text quirkDescriptionText;

        [UnityEventBinding]
        public void OnUpdateInfo(FamilyMember member)
        {
            if(nameText != null)
            {
                nameText.text = member?.Name;
            }

            if(relationshipText != null)
            {
                relationshipText.text = member?.RelationshipName;
            }

            if(wantsText != null)
            {
                wantsText.text = $"wants {member?.GiftRequest.DescriptionText}";
            }

            if(quirkTitleText != null)
            {
                quirkTitleText.text = string.Empty;
            }

            if(quirkDescriptionText != null)
            {
                quirkDescriptionText.text = string.Empty;
            }
        }
    }
}