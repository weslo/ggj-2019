using UnityEngine;

namespace Game.Gameplay
{
    public class Gift
    {
        public readonly string Name;
        public readonly int Cost;
        public readonly Sprite Sprite;

        public Gift(
            string name,
            int cost,
            Sprite sprite)
        {
            Name = name;
            Cost = cost;
            Sprite = sprite;
        }
    }
}