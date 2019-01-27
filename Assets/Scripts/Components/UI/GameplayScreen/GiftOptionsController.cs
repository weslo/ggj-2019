using UnityEngine;
using Game.Attributes;
using Game.Components.Scheduling;
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
                    button.Gift = gift;
                    button.HappinessLevel = selectedFamilyMember.GiftRequest.GiftOptions[gift];

                    Gift selectedGift = GameplayController
                        .Instance
                        .GetSelectedGift(selectedFamilyMember);

                    button.Button.interactable = selectedGift == null;
                    
                    Vector3 localPosition = RectTransform.rect.center + Vector2.right * (distanceBetweenGiftButtons * index - (distanceBetweenGiftButtons * (giftButtonPool.Count - 1)) / 2);

                    if(selectedGift == null)
                    {
                        button.AssignOnClick(() => 
                        {
                            GameplayController.Instance.SetSelectedGift(selectedFamilyMember, gift);
                        });
                    }
                    else if(button.Gift == selectedGift)
                    {
                        localPosition += Vector3.up * purchasedGiftHeightModifer;
                    }

                    TransformTweens.TweenLocalPosition(
                        transform: button.RectTransform,
                        localPosition: localPosition,
                        duration: TransformTweens.QuickTweenDuration,
                        id: this);
                });
        }

        void Start()
        {
            GameplayController.Instance.OnGiftSelected += UpdateFamilyMemberSelectedGift;
        }

        void OnDestroy()
        {
            GameplayController.Instance.OnGiftSelected -= UpdateFamilyMemberSelectedGift;
            TweenManager.Cancel(this);
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