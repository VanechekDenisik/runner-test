using System;
using UnityEngine;

namespace Core.Events
{
    public class FuncAsset<TResult> : ScriptableObject
    {
        public const string RawPath = "GAME/Funcs/";

        public event Func<TResult> OnInvoke;

        public TResult Invoke()
        {
            if (OnInvoke != null)
                return OnInvoke.Invoke();

            return default;
        }
    }

    public class FuncAsset<T, TResult> : ScriptableObject
    {
        public const string RawPath = "GAME/Funcs/";

        public event Func<T, TResult> OnInvoke;

        public TResult Invoke(T value)
        {
            if (OnInvoke != null)
                return OnInvoke.Invoke(value);

            return default;
        }
    }

    public class FuncAsset<T1, T2, TResult> : ScriptableObject
    {
        public const string RawPath = "GAME/Funcs/";

        public event Func<T1, T2, TResult> OnInvoke;

        public TResult Invoke(T1 value1, T2 value2)
        {
            if (OnInvoke != null)
                return OnInvoke.Invoke(value1, value2);

            return default;
        }
    }

    public class FuncAsset<T1, T2, T3, TResult> : ScriptableObject
    {
        public const string RawPath = "GAME/Funcs/";

        public event Func<T1, T2, T3, TResult> OnInvoke;

        public TResult Invoke(T1 value1, T2 value2, T3 value3)
        {
            if (OnInvoke != null)
                return OnInvoke.Invoke(value1, value2, value3);

            return default;
        }
    }
}