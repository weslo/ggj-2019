using UnityEngine;

namespace Game.Definitions
{
    public enum QuirkID
    {
        None = 0,
        Braggart = 1,
    }

    [CreateAssetMenu(
        fileName = "NewQuirkDefinition",
        menuName = "Game/Quirk")]
    public sealed class QuirkDefinition : ScriptableObject
    {
        [SerializeField]
        private QuirkID quirkID = default(QuirkID);
        public QuirkID QuirkID
            => quirkID;

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