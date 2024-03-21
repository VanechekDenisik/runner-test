using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Common.Entities
{
    [DefaultExecutionOrder(-100)]
    public class Entity : EntityComponent
    {
        [field: SerializeField] public EntityConfig Config { get; set; }

        private void Awake()
        {
            foreach (var component in GetComponents<Component>())
            {
                if (component == null) continue;
                InjectFieldsOf(component);
            }
        }

        private BindingFlags BindingFlags => BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        private void InjectFieldsOf(Component component)
        {
            foreach (var field in FieldsOf(component))
            {
                if (!HasInjectAttribute(field)) continue;
                if (field.FieldType.IsArray)
                {
                    var elementType = field.FieldType.GetElementType();
                    var value = GetComponents(elementType);
                    field.SetValue(component, ConvertArray(value, elementType));
                }
                else
                {
                    field.SetValue(component, GetComponent(field.FieldType));
                }
            }
        }

        private static Array ConvertArray(object[] objectArray, Type convertToType)
        {
            var castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(convertToType);
            var toArrayMethod = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(convertToType);

            var castedObject = castMethod.Invoke(null, new object[] { objectArray });
            return (Array)toArrayMethod.Invoke(null, new[] { castedObject });
        }

        private IEnumerable<FieldInfo> FieldsOf(Component component)
        {
            return GetAllFields(component.GetType(), BindingFlags);
        }

        private bool HasInjectAttribute(FieldInfo field)
        {
            return Attribute.IsDefined(field, typeof(InjectFromEntityAttribute));
        }

        private IEnumerable<FieldInfo> GetAllFields(Type t, BindingFlags bindingFlags)
        {
            if (t == null) return Enumerable.Empty<FieldInfo>();

            var result = t.GetFields(bindingFlags);
            return result.Concat(GetAllFields(t.BaseType, bindingFlags));
        }

        private new object[] GetComponents(Type type)
        {
            var result = new List<object>();

            if (type.IsInterface || type.IsSubclassOf(typeof(Component)))
                result.AddRange(base.GetComponents(type));

            if (Config != null)
                result.AddRange(Config.GetComponents(type));

            return result.ToArray();
        }

        private new object GetComponent(Type type)
        {
            if (type.IsInterface || type.IsSubclassOf(typeof(Component)))
                if (TryGetComponent(type, out var component))
                    return component;

            if (Config == null) return default;
            return Config.GetComponent(type);
        }
    }
}