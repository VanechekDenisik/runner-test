using Core.Common.Initializers.Listeners;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Common.Initializers
{
    /// <summary>
    ///     Spawns prefab using addressables and resourcePath using given transform on scene.
    /// </summary>
    [CreateAssetMenu(menuName = AssetsPaths.AssetsCore + "Addressable Initializer")]
    public class AddressableInitializer : Initializer, IDependencyListener<Transform>
    {
        [SerializeField] private AssetReferenceGameObject assetReference;
        private Transform _dummy;

        private Resource<Transform> _resource;

        public void InjectDependency(Transform value)
        {
            _dummy = value;
        }

        public override void Initialize()
        {
            _resource = new Resource<Transform>(assetReference);
            var parent = _dummy == null ? null : _dummy.parent;
            _resource.LoadAndInstantiate(OnLoaded, parent);
        }

        private void OnLoaded(ResourceLoadStatus status, Transform obj)
        {
            if (_dummy != null)
            {
                obj.SetSiblingIndex(_dummy.GetSiblingIndex());
                Destroy(_dummy.gameObject);
            }

            OnInitializationComplete();
        }
    }
}