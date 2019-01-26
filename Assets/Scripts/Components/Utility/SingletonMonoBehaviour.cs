using System;
using UnityEngine;

namespace Game.Components.Utility
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
        where T : SingletonMonoBehaviour<T>
    {
        public static T Instance
        {
            get;
            private set;
        }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                throw new Exception($"Cannot instantiate another instance of {typeof(T).ToString()}.");
            }

            Instance = this as T;
        }
    }
}