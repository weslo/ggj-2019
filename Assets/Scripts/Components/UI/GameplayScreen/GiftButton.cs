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
    }
}