using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UnityExtensions.Scenes
{
    public static class SceneFunctions
    {
        public static ScenePromise LoadScene(
            string scene,
            LoadSceneMode mode = LoadSceneMode.Additive)
        {
            if(string.IsNullOrEmpty(scene))
            {
                throw new Exception("Scene to load cannot be null.");
            }

            var promise = new ScenePromise();

            Async.Coroutine.Start(
                LoadSceneAsync(
                    scene,
                    mode,
                    (progress) => promise.UpdateProgress(progress),
                    () => promise.Resolve()));

            return promise;
        }

        public static ScenePromise UnloadScene(string scene)
        {
            if(string.IsNullOrEmpty(scene))
            {
                throw new Exception("Scene to unload cannot be null.");
            }

            var promise = new ScenePromise();

            Async.Coroutine.Start(
                UnloadSceneAsync(
                    scene,
                    (progress) => promise.UpdateProgress(progress),
                    () => promise.Resolve()));

            return promise;
        }

        public static ScenePromise TransitionScene(
            string from,
            string to)
        {
            if(from == null)
            {
                throw new Exception("Scene to transition from cannot be null.");
            }

            if(to == null)
            {
                throw new ApplicationException("Scene to transition to cannot be null.");
            }

            var promise = new ScenePromise();

            LoadScene(to)
                .OnProgress(progress =>
                    promise.UpdateProgress(progress / 2))
                .Then(() =>
                    UnloadScene(from)
                        .OnProgress(progress =>
                            promise.UpdateProgress(0.5f + progress / 2))
                        .Then(() =>
                            promise.Resolve()));

            return promise;
        }

        private static IEnumerator LoadSceneAsync(
            string scene,
            LoadSceneMode mode,
            Action<float> onProgress,
            Action onLoad)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene, mode);

            do
            {
                yield return null;
                onProgress?.Invoke(operation.progress);
            } while(!operation.isDone);

            onLoad?.Invoke();
        }

        private static IEnumerator UnloadSceneAsync(
            string scene,
            Action<float> onProgress,
            Action onUnload)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);

            do
            {
                yield return null;
                onProgress?.Invoke(operation.progress);
            } while(!operation.isDone);

            onUnload?.Invoke();
        }
    }
}