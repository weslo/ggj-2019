using UnityEngine;
using UnityEngine.UI;
using Game.Components.UI.Abstract;
using Game.Gameplay;

namespace Game.Components.UI.GameplayScreen
{
    public sealed class GiftButton : UIButton
    {
        [SerializeField]
        private Image iconImage = default(Image);

        [SerializeField]
        private Text costText = default(Text);

        [SerializeField]
        private Image happinessImage = default(Image);
        
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
                    }

                    if(costText != null)
                    {
                        costText.text = GameplayText.GetCostText(_gift.Cost);
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
    }
}