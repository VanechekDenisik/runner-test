using System;
using UnityEngine;

namespace Core.Events
{
    public class Parameter<T> : ScriptableObject
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnSet?.Invoke(_value);
            }
        }

        public event Action<T> OnSet;

        public void Subscribe(Action<T> onSet)
        {
            OnSet += onSet;
            onSet?.Invoke(_value);
        }
    }
}