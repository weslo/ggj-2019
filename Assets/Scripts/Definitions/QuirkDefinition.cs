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

        [SerializeField]
        private string descriptionText;
    }
}