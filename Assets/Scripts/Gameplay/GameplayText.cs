namespace Game.Gameplay
{
    public static class GameplayText
    {
        public static string GetWantsText(string giftRequestDescriptionText)
        {
            return $"wants {giftRequestDescriptionText}";
        }
        public static string GetCostText(int cost)
        {
            return $"${cost}";
        }
    }
}