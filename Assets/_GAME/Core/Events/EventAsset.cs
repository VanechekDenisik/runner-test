using System;
using UnityEngine;

namespace Core.Events
{
    [CreateAssetMenu(menuName = EventsAssetsPaths.Events + "Void", order = -1)]
    public class EventAsset : ScriptableObject
    {
        public event Action OnInvoked;

        public void Invoke()
        {
            OnInvoked?.Invoke();
        }
    }

    public class EventAsset<T> : ScriptableObject
    {
        public event Action<T> OnInvoked;

        public void Invoke(T value)
        {
            OnInvoked?.Invoke(value);
        }
    }

    public class EventAsset<T1, T2> : ScriptableObject
    {
        public event Action<T1, T2> OnInvoked;

        public void Invoke(T1 value1, T2 value2)
        {
            OnInvoked?.Invoke(value1, value2);
        }
    }

    public class EventAsset<T1, T2, T3> : ScriptableObject
    {
        public event Action<T1, T2, T3> OnInvoked;

        public void Invoke(T1 value1, T2 value2, T3 value3)
        {
            OnInvoked?.Invoke(value1, value2, value3);
        }
    }
}