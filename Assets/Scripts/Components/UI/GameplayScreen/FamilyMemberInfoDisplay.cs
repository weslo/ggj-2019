using UnityEngine;
using UnityEngine.UI;
using Game.Attributes;
using Game.Components.UI.Abstract;
using Game.Gameplay;

namespace Game.Components.UI.GameplayScreen
{
    public sealed class FamilyMemberInfoDisplay : UIMonoBehaviour
    {
        [SerializeField]
        private Text nameText = default(Text);

        [SerializeField]
        private Text relationshipText = default(Text);

        [SerializeField]
        private Text wantsText = default(Text);

        [SerializeField]
        private Text quirkTitleText = default(Text);

        [SerializeField]
        private Text quirkDescriptionText = default(Text);

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
                wantsText.text = GameplayText.GetWantsText(member?.GiftRequest.DescriptionText);
            }

            if(quirkTitleText != null)
            {
                quirkTitleText.text = member?.Quirk.Name;
            }

            if(quirkDescriptionText != null)
            {
                quirkDescriptionText.text = member?.Quirk.Description;
            }
        }
    }
}