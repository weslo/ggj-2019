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
                QuirkDefinition quirk = familyMemberCategory.QuirkOptions.PickRandom();

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
                    quirk: new BraggartQuirk(quirk.NameText, quirk.DescriptionText)
                ));
            });

            return members.AsReadOnly();
        }
    }
}