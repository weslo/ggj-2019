using System.Collections.ObjectModel;

namespace Game.Gameplay
{
    public class GiftRequest
    {
        public readonly string DescriptionText;
        public readonly ReadOnlyDictionary<Gift, HappinessLevel> GiftOptions;

        public GiftRequest(
            string descriptionText,
            ReadOnlyDictionary<Gift, HappinessLevel> giftOptions)
        {
            DescriptionText = descriptionText;
            GiftOptions = giftOptions;
        }
    }
}