using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewQuirkDefinition",
        menuName = "Game/Quirk")]
    public sealed class QuirkDefinition : ScriptableObject
    {
        [SerializeField]
        private string nameText = default(string);
        public string NameText
            => nameText;

        [SerializeField]
        private string descriptionText = default(string);
        public string DescriptionText
            => descriptionText;
    }
}