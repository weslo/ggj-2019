using System;

namespace Game.UnityExtensions.Scenes
{
    public class ScenePromise : Game.UnityExtensions.Async.Promise
    {
        private event Action<float> onProgress;

        public ScenePromise OnProgress(Action<float> callback)
        {
            if(callback != null)
            {
                onProgress += callback;
            }

            return this;
        }

        public ScenePromise UpdateProgress(float progress)
        {
            onProgress?.Invoke(progress);
            return this;
        }
    }
}