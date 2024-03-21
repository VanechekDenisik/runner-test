using System.Linq;
using Core.Helpers;
using UnityEngine;

namespace Core.Common.Entities
{
    public abstract class EntitiesConfigsGroup : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public EntityConfig[] Configs { get; private set; }
        [field: SerializeField] public string DescriptionText { get; private set; }

        public virtual string Description()
        {
            return DescriptionText;
        }
        
        public T[] TakeNewConfigs<T>(int count, T[] existingConfigs) where T : EntityConfig
        {
            return NewConfigs(existingConfigs).Shuffled().Take(count).ToArray();
        }

        private T[] NewConfigs<T>(T[] existingConfigs) where T : EntityConfig
        {
            return Configs.Where(s => !existingConfigs.Contains(s)).Select(c => c as T).ToArray();
        }
    }
}