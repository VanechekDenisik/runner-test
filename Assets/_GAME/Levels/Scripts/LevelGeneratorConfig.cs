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

        public LevelPart RandomPart()
        {
            return parts.Random();
        }

        public CollectableConfig RandomCollectable()
        {
            if (!RandomHelper.Random(collectableChancePercentage / 100f)) return null;
            return collectables.Random();
        }
    }
}