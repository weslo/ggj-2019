using UnityEngine;

namespace Game.Components.Scheduling
{
    public static class TransformTweens
    {
        public const float QuickTweenDuration = 0.1f;

        public static TweenManager.TimerInstance TweenLocalPosition(
            Transform transform,
            Vector3 localPosition,
            float duration,
            float delay = 0,
            object id = null)
        {
            Vector3 startPosition = transform.localPosition;
            return TweenManager.Tween(duration, delay, id)
                .OnStep(t =>
                {
                    transform.localPosition = Vector3.Lerp(startPosition, localPosition, t);
                });
        }

        public static TweenManager.TimerInstance TweenLocalScale(
            Transform transform,
            Vector3 localScale,
            float duration,
            float delay = 0,
            object id = null)
        {
            Vector3 startScale = transform.localScale;
            return TweenManager.Tween(duration, delay, id)
                .OnStep(t =>
                {
                    transform.localScale = Vector3.Lerp(startScale, localScale, t);
                });
        }

        public static TweenManager.TimerInstance TweenRectTransformSize(
            RectTransform rectTransform,
            Vector2 size,
            float duration,
            float delay = 0,
            object id = null)
        {
            Vector2 startSize = rectTransform.sizeDelta;
            return TweenManager.Tween(duration, delay, id)
                .OnStep(t =>
                {
                    rectTransform.sizeDelta = Vector2.Lerp(startSize, size, t);
                });
        }
    }
}