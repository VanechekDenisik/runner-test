using Collectables;
using Core.Common.Entities;
using Core.Helpers;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(menuName = LevelsAssetsPaths.Assets + "Generator")]
    public class LevelGeneratorConfig : EntityConfig
    {
        [SerializeField] private LevelPart[] parts;
        [SerializeField] private CollectableConfig[] collectables;
        [SerializeField] private float collectableChancePercentage;

        //Using such methods here improves encapsulation and simplifies understanding of the class.
        //I used "Move Method" refactoring patter here.
        public LevelPart SpawnRandomPart(Transform parent, Vector3 position)
        {
            var result = Instantiate(parts.Random(), parent);
            result.transform.position = position;
            result.gameObject.SetActive(true);
            return result;
        }

        //And here :)
        public CollectableConfig RandomCollectable()
        {
            if (!RandomHelper.Random(collectableChancePercentage / 100f)) return null;
            return collectables.Random();
        }
    }
}