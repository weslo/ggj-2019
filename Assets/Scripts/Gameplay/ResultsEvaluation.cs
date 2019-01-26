using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Gameplay
{
    public static class ResultsEvaluation
    {
        public static EndOfGameResults EvaluateGameResults(ReadOnlyDictionary<FamilyMember, Gift> choices)
        {
            return new EndOfGameResults(
                familyMemberResults: choices
                    .Select(kvp => new FamilyMemberResult(
                        familyMember: kvp.Key,
                        gift: kvp.Value,
                        happinessLevel: HappinessLevel.Satisfied))
                    .ToArray(),
                score: 1000);
        }
    }
}