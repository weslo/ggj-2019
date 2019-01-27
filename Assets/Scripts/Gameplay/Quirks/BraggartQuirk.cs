using System.Collections.Generic;
using Game.Components.UI;
using Game.CSharpExtensions;

namespace Game.Gameplay.Quirks
{
    public class BraggartQuirk : AbstractQuirk
    {
        public override void ApplyChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberResult> results,
            int score,
            out int modifiedScore)
        {
            modifiedScore = score + ResultsEvaluation.HappinessMultiplier;
        }

        public override void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits)
        {
            portraits.ForEach((member, portrait) =>
            {
                if(member == source)
                {
                    portrait.PlayEnterAnimation();
                }
            });
        }
    }
}