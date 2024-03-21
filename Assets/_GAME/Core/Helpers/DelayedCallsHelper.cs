using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Helpers
{
    /// <summary>
    ///     Contains helpful methods to deal with delayed calls of methods
    /// </summary>
    public static class DelayedCallsHelper
    {
        private static readonly Dictionary<float, WaitForSeconds> _secondsToWaitForSeconds = new();

        public static Coroutine DelayedCall(this MonoBehaviour component, double delay, Action action,
            bool ignoreTimeScale = false)
        {
            return DelayedCall(component, (float)delay, action, ignoreTimeScale);
        }

        public static Coroutine DelayedCall(this MonoBehaviour component, float delay, Action action,
            bool ignoreTimeScale = false)
        {
            if (delay <= 0)
            {
                action?.Invoke();
                return null;
            }

            return component.StartCoroutine(Invoke(delay, action, ignoreTimeScale));
        }

        public static Coroutine SkipFrame(this MonoBehaviour component, Action action)
        {
            return SkipFrames(component, 1, action);
        }

        public static Coroutine SkipFrames(this MonoBehaviour component, int framesCount, Action action)
        {
            return component.StartCoroutine(SkipFramesCoroutine(framesCount, action));
        }

        private static IEnumerator Invoke(float delay, Action action, bool ignoreTimeScale = false)
        {
            if (ignoreTimeScale)
                yield return new WaitForSecondsRealtime(delay);
            else
                yield return new WaitForSeconds(delay);

            action?.Invoke();
        }

        private static IEnumerator SkipFramesCoroutine(int framesCount, Action action)
        {
            for (var frameId = 0; frameId < framesCount; frameId++)
                yield return null;

            action?.Invoke();
        }

        public static WaitForSeconds ToWaitTime(this float seconds)
        {
            if (!_secondsToWaitForSeconds.ContainsKey(seconds))
                _secondsToWaitForSeconds.Add(seconds, new WaitForSeconds(seconds));

            return _secondsToWaitForSeconds[seconds];
        }
    }
}