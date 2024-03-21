using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Core.Common.Initializers
{
    /// <summary>
    ///     The class for convenient resource load from unity addressables system
    /// </summary>
    public class Resource
    {
        public Resource(AssetReferenceGameObject assetReference)
        {
            AssetReference = assetReference;
        }

        public GameObject Content { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool IsLoading { get; private set; }
        public bool IsLoadingOrLoaded => IsLoading || IsLoaded;

        protected string Path { get; }
        protected AssetReferenceGameObject AssetReference { get; }
        public event Action<GameObject> OnLoaded;
        public event Action<GameObject> OnLoadedAndInstantiated;

        public void Load(Action<ResourceLoadStatus, GameObject> callback)
        {
            CoroutinesSingleton.StartCoroutine(LoadInternal(callback));
        }

        public void LoadAndInstantiate(Action<ResourceLoadStatus, GameObject> callback, Transform handle = null)
        {
            Load((status, component) =>
            {
                if (status == ResourceLoadStatus.Complete)
                {
                    Content = Object.Instantiate(component, handle);
                    OnLoadedAndInstantiatedInvoker(Content);
                }

                callback?.Invoke(status, Content);
            });
        }

        private IEnumerator LoadInternal(Action<ResourceLoadStatus, GameObject> callback)
        {
            IsLoading = true;
            var handle = Addressables.LoadAssetAsync<GameObject>(AssetReference);
            yield return handle;
            IsLoading = false;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Content = handle.Result;
                IsLoaded = true;

                callback?.Invoke(ResourceLoadStatus.Complete, Content);
                OnLoaded?.Invoke(Content);
            }
            else
            {
                Debug.LogError($"Error while loading resource on path: {Path}");
                callback?.Invoke(ResourceLoadStatus.Failed, null);
            }
        }

        protected void OnLoadedAndInstantiatedInvoker(GameObject content)
        {
            OnLoadedAndInstantiated?.Invoke(content);
        }
    }
}