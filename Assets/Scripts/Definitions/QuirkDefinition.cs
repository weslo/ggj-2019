using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu(
        fileName = "NewQuirkDefinition",
        menuName = "Game/Quirk")]
    public sealed class QuirkDefinition : ScriptableObject
    {
        [SerializeField]
        private string nameText;
        public string NameText
            => nameText;

        [SerializeField]
        private string descriptionText;
        public string DescriptionText
            => descriptionText;
    }
}