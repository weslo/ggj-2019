using System.Collections.Generic;
using Game.Components.UI;

namespace Game.Gameplay.Quirks
{
    public abstract class AbstractQuirk
    {
        public static readonly object QuirkSchedulingKey = new object();
        
        public virtual void ApplyChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberResult> results,
            int score,
            out int modifiedScore)
        {
            modifiedScore = score;
        }

        public virtual void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits)
        {
            portraits[source].PlayQuirkPingAnimation();
        }
    }
}