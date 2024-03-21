using System.Linq;

namespace Core.Common.Entities
{
    public abstract class EntitiesConfigsGroupFor<T> : EntitiesConfigsGroup where T : EntityConfig
    {
        public T[] List => Configs.Select(c => c as T).ToArray();
    }
}