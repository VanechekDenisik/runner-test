using System.Collections.Generic;
using Core.Common.Initializers.Listeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Common.Initializers
{
    [DefaultExecutionOrder(-200)]
    public abstract class DependencyInjector<T> : MonoBehaviour
    {
        [SerializeField] [ValidateInput("Validate")]
        private List<ScriptableObject> initializers;

        private void Awake()
        {
            var component = GetComponent<T>();

            foreach (var initializer in initializers)
            {
                if (initializer is not IDependencyListener<T> listener)
                {
                    Debug.LogError(
                        $"{initializer.name} does not implement an interface {typeof(IDependencyListener<T>)}");
                    continue;
                }

                listener.InjectDependency(component);
            }
        }

        private bool Validate()
        {
            if (initializers == null)
                return false;

            foreach (var initializer in initializers)
                if (initializer is not IDependencyListener<T>)
                    return false;

            return true;
        }
    }
}