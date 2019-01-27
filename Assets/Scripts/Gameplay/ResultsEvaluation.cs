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
                HappinessLevel happiness = kvp.Key.GiftRequest.GiftOptions.Get(kvp.Value, fallback: HappinessLevel.NoGift);
                score += (int)happiness * happinessMultiplier;
                familyMemberResults.Add(new FamilyMemberResult(
                    familyMember: kvp.Key,
                    gift: kvp.Value,
                    happinessLevel: happiness));
            }

            return new EndOfGameResults(
                familyMemberResults: familyMemberResults.ToArray(),
                score: score);
        }
    }
}