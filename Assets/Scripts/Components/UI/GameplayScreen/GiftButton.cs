using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Components.Scheduling;
using Game.Components.UI.Abstract;
using Game.Gameplay;

namespace Game.Components.UI.GameplayScreen
{
    public sealed class GiftButton
        : UIButton
        , IPointerEnterHandler
        , IPointerExitHandler
    {

        [Header("Object References")]

        [SerializeField]
        private Image iconImage = default(Image);

        [SerializeField]
        private Text costText = default(Text);

        [SerializeField]
        private Text giftNameText = default(Text);

        [SerializeField]
        private Image happinessImage = default(Image);

        [Header("Transformations")]

        [SerializeField]
        private float hoverScale = default(float);

        [SerializeField]
        private float hoverVerticalDelta = default(float);

        private Vector2 idlePosition;

        private Vector2 idleSize;

        private Vector2 hoverPosition;
        
        private Vector2 hoverSize;
        
        private Gift _gift;
        public Gift Gift
        {
            get => _gift;
            set
            {
                if(_gift != value)
                {
                    _gift = value;

                    if(iconImage != null)
                    {
                        iconImage.sprite = _gift.Sprite;
                        iconImage.GraphicUpdateComplete();
                    }

                    if(costText != null)
                    {
                        costText.text = GameplayText.GetCostText(_gift.Cost);
                    }

                    if(giftNameText != null)
                    {
                        giftNameText.text = _gift.Name;
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
                        happinessImage.sprite = HappinessIcons.Instance.GetHappinessLevelSprite(_happinessLevel);
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            idlePosition = RectTransform.localPosition;
            idleSize = RectTransform.sizeDelta;

            hoverPosition = idlePosition + Vector2.up * hoverVerticalDelta;
            hoverSize = idleSize * hoverScale;
        }

        void OnDestroy()
        {
            TweenManager.Cancel(this);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if(giftNameText != null)
            {
                giftNameText.enabled = true;
            }
            
            RectTransform.SetAsLastSibling();

            TweenManager.Cancel(this);
            TransformTweens.TweenRectTransformSize(
                rectTransform: RectTransform,
                size: hoverSize,
                duration: TransformTweens.QuickTweenDuration,
                id: this);
        }

        public void OnPointerExit(PointerEventData data)
        {
            if(giftNameText != null)
            {
                giftNameText.enabled = false;
            }

            TweenManager.Cancel(this);
            TransformTweens.TweenRectTransformSize(
                rectTransform: RectTransform,
                size: idleSize,
                duration: TransformTweens.QuickTweenDuration,
                id: this);
        }
    }
}