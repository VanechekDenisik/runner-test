using System;
using System.Collections.Generic;
using System.Linq;
using Core.Helpers;
using UnityEngine;

namespace Core.Common.Entities
{
    public class EntityConfig : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> components;
        
        [field: SerializeField] public EntitiesConfigsGroup[] Groups { get; private set; }

        public T GetComponent<T>()
        {
            return (T)GetComponent(typeof(T));
        }
        
        public object GetComponent(Type type)
        {
            if (type.IsInstanceOfType(this)) return this;

            foreach (var component in components)
                if (type.IsInstanceOfType(component))
                    return component;
            
            return default;
        }
        
        public T[] GetComponents<T>()
        {
            return GetComponents(typeof(T)).Select(c => (T)c).ToArray();
        }

        public object[] GetComponents(Type type)
        {
            var result = new List<object>();
            if (type.IsInstanceOfType(this)) result.Add(this);

            foreach (var component in components)
                if (type.IsInstanceOfType(component))
                    result.Add(component);
            
            return result.ToArray();
        }
        
        public string[] GetDescriptions()
        {
            return GetComponents<IDescription>()
                .SelectMany(d => d.DescriptionSections)
                .OrderByDescending(s => s.Priority)
                .Select(s => s.Description).ToArray();
        }
        
        public string GetDescriptionsMerged(string separator)
        {
            return GetDescriptions().MergeStrings(separator);
        }
    }
}