using System.Collections.ObjectModel;

namespace Game.Gameplay
{
    public class GiftRequest
    {
        public readonly string DescriptionText;
        public readonly ReadOnlyCollection<Gift> GiftOptions;

        public GiftRequest(
            string descriptionText,
            ReadOnlyCollection<Gift> giftOptions)
        {
            DescriptionText = descriptionText;
            GiftOptions = giftOptions;
        }
    }
}