using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.UI.Abstract
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : UIMonoBehaviour
    {
        public Button Button
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();
            Button = GetComponent<Button>();
        }

        public UIButton AssignOnClick(Action handler)
        {
            Button.onClick.RemoveAllListeners();
            if(handler != null)
            {
                Button.onClick.AddListener(handler.Invoke);
            }

            return this;
        }
    }
}