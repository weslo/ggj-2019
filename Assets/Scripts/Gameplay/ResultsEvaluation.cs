using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.CSharpExtensions;

namespace Game.Gameplay
{
    public static class ResultsEvaluation
    {
        private const int happinessMultiplier = 100;

        public static EndOfGameResults EvaluateGameResults(ReadOnlyDictionary<FamilyMember, Gift> choices)
        {
            var familyMemberResults = new List<FamilyMemberResult>();
            int score = 0;

            foreach(KeyValuePair<FamilyMember, Gift> kvp in choices)
            {
                FamilyMember member = kvp.Key;
                Gift gift = kvp.Value;
                HappinessLevel happiness = GetHappinessLevelForGiftChoice(member, gift);

                score += CalculateBaseScoreForGiftChoice(member, gift);
                familyMemberResults.Add(new FamilyMemberResult(
                    familyMember: member,
                    gift: gift,
                    happinessLevel: happiness));
            }

            return new EndOfGameResults(
                familyMemberResults: familyMemberResults.ToArray(),
                score: score);
        }

        public static HappinessLevel GetHappinessLevelForGiftChoice(FamilyMember member, Gift gift)
        {
            return member.GiftRequest.GiftOptions.Get(gift, fallback: HappinessLevel.NoGift);
        }

        public static int CalculateBaseScoreForGiftChoice(FamilyMember member, Gift gift)
        {
            HappinessLevel happiness = GetHappinessLevelForGiftChoice(member, gift);
            return (int)happiness * happinessMultiplier;
        }
    }
}