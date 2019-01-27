using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.CSharpExtensions;
using Game.Definitions;
using Game.Gameplay.Quirks;

namespace Game.Gameplay
{
    public static class FamilyMemberGeneration
    {
        public static ReadOnlyCollection<FamilyMember> GenerateFamilyMembers(
            FamilyMemberGeneratorDefinition generator,
            int numFamilyMembers,
            HappinessLevel[] giftHappinessOptions)
        {
            var members = new List<FamilyMember>();

            Functions.Repeat(numFamilyMembers, () =>
            {
                FamilyMemberCategoryDefinition familyMemberCategory = generator.Categories.PickRandom();
                GiftCategoryDefinition giftCategory = familyMemberCategory.GiftCategoryOptions.PickRandom();

                members.Add(new FamilyMember(
                    name: familyMemberCategory.NameOptions.PickRandom(),
                    relationshipName: familyMemberCategory.RelationshipNameOptions.PickRandom(),
                    portraitSprite: familyMemberCategory.PortraitOptions.PickRandom(),
                    giftRequest: new GiftRequest(
                        descriptionText: giftCategory.DescriptionText,
                        giftOptions: giftCategory.GiftOptions
                            .PickRandomUnique(giftHappinessOptions.Length)
                            .OrderBy(def => def.Cost)
                            .Select(def => new Gift(
                                name: def.NameText,
                                cost: def.Cost,
                                sprite: def.Sprite))
                            .Map((gift, i) => giftHappinessOptions[i])
                            .AsReadOnly()),
                    quirk: GetQuirk(familyMemberCategory.QuirkOptions.PickRandom())
                ));
            });

            return members.AsReadOnly();
        }

        private static AbstractQuirk GetQuirk(QuirkDefinition definition)
        {
            switch(definition.QuirkID)
            {
                case QuirkID.Braggart: return new BraggartQuirk(definition.NameText, definition.DescriptionText);
                case QuirkID.Complainer: return new ComplainerQuirk(definition.NameText, definition.DescriptionText);
                case QuirkID.Complimenter: return new ComplimenterQuirk(definition.NameText, definition.DescriptionText);
                case QuirkID.Sharing: return new SharingQuirk(definition.NameText, definition.DescriptionText);
                default: throw new Exception($"Quirk {definition.QuirkID.ToString()} does not have a handler.");
            }
        }
    }
}