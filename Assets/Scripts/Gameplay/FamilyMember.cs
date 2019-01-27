using UnityEngine;
using Game.Gameplay.Quirks;

namespace Game.Gameplay
{
    public class FamilyMember
    {
        public readonly string Name;
        public readonly string RelationshipName;
        public readonly Sprite PortraitSprite;
        public readonly GiftRequest GiftRequest;
        public readonly AbstractQuirk Quirk;

        public FamilyMember(
            string name,
            string relationshipName,
            Sprite portraitSprite,
            GiftRequest giftRequest,
            AbstractQuirk quirk)
        {
            Name = name;
            RelationshipName = relationshipName;
            PortraitSprite = portraitSprite;
            GiftRequest = giftRequest;
            Quirk = quirk;
        }
    }
}