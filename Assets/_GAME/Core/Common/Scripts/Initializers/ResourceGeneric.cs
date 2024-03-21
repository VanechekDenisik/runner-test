using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core.Common.Initializers
{
    public class Resource<T> : Resource where T : Component
    {
        public Resource(AssetReferenceGameObject assetReference) : base(assetReference)
        {
        }

        public new T Content { get; private set; }

        public void Load(Action<ResourceLoadStatus, T> callback)
        {
            base.Load((status, o) =>
            {
                if (status == ResourceLoadStatus.Complete)
                {
                    var component = o.GetComponent<T>();
                    Content = component;
                }

                callback?.Invoke(status, Content);
            });
        }

        public void LoadAndInstantiate(Action<ResourceLoadStatus, T> callback, Transform handle = null)
        {
            Load((status, component) =>
            {
                if (status == ResourceLoadStatus.Complete)
                {
                    Content = Object.Instantiate(component, handle);
                    OnLoadedAndInstantiatedInvoker(Content.gameObject);
                }

                callback?.Invoke(status, Content);
            });
        }
    }
}