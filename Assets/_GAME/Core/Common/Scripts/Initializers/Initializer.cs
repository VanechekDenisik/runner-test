using System;
using UnityEngine;

namespace Core.Common.Initializers
{
    public abstract class Initializer : ScriptableObject
    {
        public bool IsInitialized { get; private set; }
        public event Action<Initializer> OnInitialized;

        public abstract void Initialize();

        protected void OnInitializationComplete()
        {
            IsInitialized = true;
            OnInitialized?.Invoke(this);
        }
    }
}