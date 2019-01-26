using UnityEngine;
using Game.Attributes;
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
                });
        }

        [UnityEventBinding]
        public void UpdateFamilyMember(FamilyMember member)
        {
            giftButtonPool.SetData(member.GiftRequest.GiftOptions);
        }
    }
}