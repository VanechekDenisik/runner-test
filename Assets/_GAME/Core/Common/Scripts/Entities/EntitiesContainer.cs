using System;
using System.Linq;
using Core.Helpers;
using UnityEngine;

namespace Core.Common.Entities
{
    public class EntitiesContainer<TEntityComponent, TEntityConfig> : EntityComponent 
        where TEntityComponent : EntityComponentWithConfig<TEntityConfig>
        where TEntityConfig : EntityConfigWithPrefab
    {
        [SerializeField] private Transform parent;

        [InjectFromEntity] private IStartingEntities<TEntityConfig> _startingEntities;
        
        public event Action<TEntityComponent> OnAdded;
        
        public TEntityComponent[] List { get; private set; } = Array.Empty<TEntityComponent>();
        public TEntityConfig[] Configs => List.Select(s => s.Config).ToArray();
        
        public void Spawn(EntityConfigWithPrefab config)
        {
            var entity = config.Spawn<TEntityComponent>(parent);
            List = List.With(entity).ToArray();
            foreach (var entityOfContainer in entity.GetComponents<IEntityOfContainer>())
                entityOfContainer.OnRegistered(this);
            OnAdded?.Invoke(entity);
        }

        protected virtual void Awake()
        {
            SpawnStartingEntities();
        }

        private void SpawnStartingEntities()
        {
            if (_startingEntities == null || _startingEntities.StartingEntities.IsEmpty())
                return;
            
            foreach (var config in _startingEntities.StartingEntities)
                Spawn(config);
        }
    }
}