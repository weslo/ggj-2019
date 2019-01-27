using System.Collections.Generic;
using Game.Components.UI;

namespace Game.Gameplay.Quirks
{
    public abstract class AbstractQuirk
    {
        public static readonly object QuirkSchedulingKey = new object();

        public readonly string Name;

        public readonly string Description;

        public AbstractQuirk(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual void AnimateChanges(
            FamilyMember source,
            Dictionary<FamilyMember, FamilyMemberResult> results,
            Dictionary<FamilyMember, FamilyMemberPortrait> portraits,
            int score,
            out int modifiedScore)
        {
            modifiedScore = score;
            portraits[source].PlayQuirkPingAnimation();
        }
    }
}