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

        public Animator Animator
        {
            get;
            private set;
        }

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Animator = GetComponent<Animator>();
        }
    }
}