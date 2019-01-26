using UnityEngine;

namespace Game.Gameplay
{
    public class FamilyMember
    {
        public readonly string Name;
        public readonly string RelationshipName;
        public readonly Sprite PortraitSprite;
        public readonly GiftRequest GiftRequest;

        public FamilyMember(
            string name,
            string relationshipName,
            Sprite portraitSprite,
            GiftRequest giftRequest)
        {
            Name = name;
            RelationshipName = relationshipName;
            PortraitSprite = portraitSprite;
            GiftRequest = giftRequest;
        }
    }
}