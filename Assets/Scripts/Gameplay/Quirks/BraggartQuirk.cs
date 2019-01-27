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
            base.ApplyChanges(source, results, score, out modifiedScore);
            modifiedScore += 100;
        }

        public override void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits)
        {
            base.AnimateChanges(source, portraits);

            portraits.ForEach((member, portrait) =>
            {
                if(member != source)
                {
                    portrait.PlayPositivePingAnimation();
                }
            });
        }
    }
}