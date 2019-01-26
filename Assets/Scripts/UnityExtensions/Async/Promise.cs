using System;

namespace Game.UnityExtensions.Async
{
    public class Promise
    {
        private event Action then;
        
        public Promise Then(Action callback)
        {
            if(callback != null)
            {
                then += callback;
            }
            return this;
        }

        public Promise Resolve()
        {
            then?.Invoke();
            return this;
        }
    }
}