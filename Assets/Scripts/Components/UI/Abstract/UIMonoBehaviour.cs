using UnityEngine;

namespace Game.Components.UI.Abstract
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIMonoBehaviour : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get;
            private set;
        }

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}