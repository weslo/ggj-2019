using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CSharpExtensions;

namespace Game.Gameplay
{
    public static class ResultsEvaluation
    {
        public const int HappinessMultiplier = 100;

        private static readonly HappinessLevel[] happinessLevelOrder = new []
        {
            HappinessLevel.Hate,
            HappinessLevel.Disappointed,
            HappinessLevel.Indifferent,
            HappinessLevel.Satisfied,
            HappinessLevel.Happy,
            HappinessLevel.Love,
            HappinessLevel.Favorite,
        };

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
            return GetHappinessScoreValue(happiness);
        }

        public static HappinessLevel IncrementHappiness(HappinessLevel level)
        {
            if(level == HappinessLevel.NoGift)
            {
                return level;
            }

            if(level == HappinessLevel.Favorite)
            {
                return level;
            }

            return happinessLevelOrder[Array.IndexOf(happinessLevelOrder, level) + 1];
        }

        public static HappinessLevel DecrementHappiness(HappinessLevel level)
        {
            if(level == HappinessLevel.NoGift)
            {
                return level;
            }

            if(level == HappinessLevel.Hate)
            {
                return level;
            }

            return happinessLevelOrder[Array.IndexOf(happinessLevelOrder, level) - 1];
        }

        public static int GetHappinessScoreValue(HappinessLevel happiness)
        {
            return (int)happiness * HappinessMultiplier;
        }

        public static int GetHappinessScoreDifference(HappinessLevel prev, HappinessLevel next)
        {
            return GetHappinessScoreValue(next) - GetHappinessScoreValue(prev);
        }
    }
}