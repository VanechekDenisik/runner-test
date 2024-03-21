using System.Collections;
using UnityEngine;

namespace Core.Common
{
    public static class CoroutinesSingleton
    {
        public static CoroutinesHandler Handler { get; private set; }

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            if (Handler == null)
            {
                var handle = new GameObject("CoroutineHandle");
                Handler = handle.AddComponent<CoroutinesHandler>();
                Object.DontDestroyOnLoad(handle.gameObject);
            }

            return Handler.StartCoroutine(routine);
        }

        public class CoroutinesHandler : MonoBehaviour
        {
        }
    }
}