using UnityEngine;

namespace Core.Common.Initializers
{
    /// <summary>
    ///     Required for custom dependency injection of specific initializer when Dependency Injector is not enough
    /// </summary>
    /// <typeparam name="T">Specific initializer type</typeparam>
    [DefaultExecutionOrder(-200)]
    public abstract class CustomDependencyInjector<T> : MonoBehaviour where T : Initializer
    {
        [SerializeField] private T initializer;

        private void Awake()
        {
            OnInjectDependency(initializer);
        }

        protected abstract void OnInjectDependency(T value);
    }
}