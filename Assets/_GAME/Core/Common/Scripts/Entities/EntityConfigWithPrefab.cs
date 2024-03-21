using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Common.Entities
{
    public class EntityConfigWithPrefab : EntityConfig
    {
        [SerializeField, FormerlySerializedAs("entityPrefab")] 
        protected Entity prefab;
        
        public Entity Spawn(Transform parent = null)
        {
            prefab.Config = this;
            var item = Instantiate(prefab, parent);
            item.name = name;
            return item;
        }
        
        public T Spawn<T>(Transform parent = null) where T : Component
        {
            if (!Spawn(parent).TryGetComponent<T>(out var component))
                throw new ArgumentException();
            return component;
        }
    }
}