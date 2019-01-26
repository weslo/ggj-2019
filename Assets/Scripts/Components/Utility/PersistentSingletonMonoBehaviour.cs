namespace Game.Components.Utility
{
    public abstract class PersistentSingletonMonoBehaviour<T> : SingletonMonoBehaviour<T>
        where T : PersistentSingletonMonoBehaviour<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}