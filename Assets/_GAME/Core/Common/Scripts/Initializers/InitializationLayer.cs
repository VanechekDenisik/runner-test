using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Common.Initializers
{
    [Serializable]
    public class InitializationLayer
    {
        [SerializeField] [HideLabel] private string layer;
        [SerializeField] private List<Initializer> initializers;

        [NonSerialized] private int _initializedCount;
        [NonSerialized] private bool _isInitialized;

        public string Layer => layer;
        public IReadOnlyList<Initializer> Initializers => initializers;
        public bool IsInitialized => _isInitialized;

        public void Initialize()
        {
            if (initializers.Count == 0)
                _isInitialized = true;

            foreach (var initializer in initializers)
            {
                initializer.OnInitialized += InitializerOnInitialized;
                initializer.Initialize();
            }
        }

        private void InitializerOnInitialized(Initializer initializer)
        {
            initializer.OnInitialized -= InitializerOnInitialized;
            _initializedCount++;

            if (_initializedCount >= initializers.Count)
                _isInitialized = true;
        }
    }
}