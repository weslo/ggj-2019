using UnityEngine;
using Game.Attributes;
using Game.Components.UI.Abstract;
using Game.Gameplay;
using Game.UnityExtensions;

namespace Game.Components.UI.GameplayScreen
{
    public sealed class GiftOptionsController : UIMonoBehaviour
    {
        [Header("Object References")]

        [SerializeField]
        private GiftButton giftButtonPrefab = default(GiftButton);

        [Header("Transformations")]

        [SerializeField]
        private float distanceBetweenGiftButtons = default(float);

        [SerializeField]
        private float purchasedGiftHeightModifer = default(float);

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
                    button.HappinessLevel = selectedFamilyMember.GiftRequest.GiftOptions[gift];

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
                    else if(button.Gift == selectedGift)
                    {
                        button.RectTransform.localPosition = button.RectTransform.localPosition + Vector3.up * purchasedGiftHeightModifer;
                    }
                });
        }

        void Start()
        {
            GameplayController.Instance.OnGiftSelected += UpdateFamilyMemberSelectedGift;
        }

        void OnDestroy()
        {
            GameplayController.Instance.OnGiftSelected -= UpdateFamilyMemberSelectedGift;
        }

        [UnityEventBinding]
        public void UpdateFamilyMember(FamilyMember member)
        {
            selectedFamilyMember = member;
            giftButtonPool.SetData(member.GiftRequest.GiftOptions.Keys);
        }

        public void UpdateFamilyMemberSelectedGift(FamilyMember member, Gift gift)
        {
            if(member == selectedFamilyMember)
            {
                giftButtonPool.SetData(member.GiftRequest.GiftOptions.Keys);
            }
        }
    }
}