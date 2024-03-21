using Core.Common.Entities;
using Core.Helpers;
using UnityEngine;

namespace Levels
{
    public class LevelGenerator : EntityComponentWithConfig<LevelGeneratorConfig>
    {
        [SerializeField] private LevelPart startingLevelPart;
        
        private void Awake()
        {
            LevelPart previousPart = startingLevelPart;
            for (var partId = 0; partId < 20; partId++)
            {
                var levelPart = Instantiate(Config.RandomPart(), transform);
                levelPart.transform.position = previousPart?.EndPoint.position ?? Vector3.zero;
                levelPart.gameObject.SetActive(true);
                
                var collectable = Config.RandomCollectable()?.Spawn(levelPart.transform);
                if (collectable != null)
                    collectable.transform.position = levelPart.CoinSpawnPoint.position;
                
                previousPart = levelPart;
            }
        }
    }
}
