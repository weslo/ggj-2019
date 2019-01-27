using System.Collections.Generic;
using Game.Components.UI;
using Game.CSharpExtensions;

namespace Game.Gameplay.Quirks
{
    public class ComplimenterQuirk : AbstractQuirk
    {
        public ComplimenterQuirk(string name, string description) : base(name, description) { }

        public override void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberResult> results,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits,
            int score,
            out int modifiedScore)
        {
            base.AnimateChanges(source, results, portraits, score, out modifiedScore);

            int outScore = modifiedScore;
            portraits.ForEach((member, portrait) =>
            {
                if(member == source)
                {
                    return;
                }
                
                Gift sourceGift = results[source].Gift;
                Gift evalGift = results[member].Gift;

                if(sourceGift.Cost < evalGift.Cost)
                {
                    HappinessLevel current = portrait.HappinessLevel;
                    HappinessLevel modified = ResultsEvaluation.IncrementHappiness(portrait.HappinessLevel);
                    int scoreDifference = ResultsEvaluation.GetHappinessScoreDifference(current, modified);

                    portrait.PlayPositivePingAnimation();
                    portrait.HappinessLevel = modified;

                    outScore += scoreDifference;
                }
            });

            modifiedScore = outScore;
        }
    }
}