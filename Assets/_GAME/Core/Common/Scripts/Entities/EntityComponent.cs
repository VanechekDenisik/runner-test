using UnityEngine;

namespace Core.Common.Entities
{
    [RequireComponent(typeof(Entity))]
    public abstract class EntityComponent : MonoBehaviour
    {
        private Entity _entity;

        public Entity Entity
        {
            get
            {
                if (_entity == null) _entity = GetComponent<Entity>();
                return _entity;
            }
        }

        public T GetConfigComponent<T>()
        {
            if (Entity.Config == null) return default;
            return Entity.Config.GetComponent<T>();
        }

        public bool IsActive => gameObject.activeInHierarchy && enabled;
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}