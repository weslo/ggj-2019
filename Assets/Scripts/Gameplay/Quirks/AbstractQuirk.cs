using System.Collections.Generic;
using Game.Components.UI;

namespace Game.Gameplay.Quirks
{
    public abstract class AbstractQuirk
    {
        public static readonly object QuirkSchedulingKey = new object();
        
        public abstract void ApplyChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberResult> results,
            int score,
            out int modifiedScore);

        public abstract void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits);
    }
}