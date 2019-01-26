namespace Game.Gameplay
{
    public enum HappinessLevel
    {
        NoGift = -3,
        Hate = -2,
        Disappointed = -1,
        Indifferent = 0,
        Satisfied = 1,
        Happy = 2,
        Love = 3,
        Favorite = 5,
    }

    public sealed class FamilyMemberResult
    {
        public readonly FamilyMember FamilyMember;
        public readonly Gift Gift;
        public readonly HappinessLevel HappinessLevel;

        public FamilyMemberResult(
            FamilyMember familyMember,
            Gift gift,
            HappinessLevel happinessLevel)
        {
            FamilyMember = familyMember;
            Gift = gift;
            HappinessLevel = happinessLevel;
        }
    }

    public sealed class EndOfGameResults
    {
        public readonly FamilyMemberResult[] FamilyMemberResults;
        public readonly int Score;

        public EndOfGameResults(
            FamilyMemberResult[] familyMemberResults,
            int score)
        {
            FamilyMemberResults = familyMemberResults;
            Score = score;
        }
    }
}