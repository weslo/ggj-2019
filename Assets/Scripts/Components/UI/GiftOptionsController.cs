using UnityEngine;
using UnityEngine.UI;
using Game.Attributes;
using Game.Components.UI.Abstract;
using Game.Gameplay;
using Game.UnityExtensions;

namespace Game.Components.UI
{
    public sealed class GiftOptionsController : UIMonoBehaviour
    {
        [Header("Object References")]

        [SerializeField]
        private GiftButton giftButtonPrefab = default(GiftButton);

        [Header("Transformations")]

        [SerializeField]
        private float distanceBetweenGiftButtons = default(float);

        private FamilyMember selectedFamilyMember;

        private ObjectPool<GiftButton, Gift> giftButtonPool;

        protected override void Awake()
        {
            base.Awake();

            giftButtonPool = new ObjectPool<GiftButton, Gift>(
                prefab: giftButtonPrefab,
                apply: (button, gift, index) =>
                {
                    button.RectTransform.SetParent(RectTransform, false);
                    button.RectTransform.localPosition = RectTransform.rect.center + Vector2.right * (distanceBetweenGiftButtons * index - (distanceBetweenGiftButtons * (giftButtonPool.Count - 1)) / 2);
                    button.Gift = gift;

                    Gift selectedGift = GameplayController
                        .Instance
                        .GetSelectedGift(selectedFamilyMember);

                    button.Button.interactable = selectedGift == null;

                    if(selectedGift == null)
                    {
                        button.AssignOnClick(() => 
                        {
                            GameplayController.Instance.SetSelectedGift(selectedFamilyMember, gift);
                        });
                    }
                });
        }

        [UnityEventBinding]
        public void UpdateFamilyMember(FamilyMember member)
        {
            selectedFamilyMember = member;
            giftButtonPool.SetData(member.GiftRequest.GiftOptions);
        }

        [UnityEventBinding]
        public void UpdateFamilyMemberSelectedGift(FamilyMember member, Gift gift)
        {
            if(member == selectedFamilyMember)
            {
                giftButtonPool.SetData(member.GiftRequest.GiftOptions);
            }
        }
    }
}