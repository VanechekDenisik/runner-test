using Bonuses;
using Core.Common.Entities;
using UnityEngine;

namespace Collectables
{
    [CreateAssetMenu(menuName = CollectablesAssetsPaths.Assets + "Config")]
    public class CollectableConfig : EntityConfigWithPrefab
    {
        [field: SerializeField] public Bonus[] Bonuses { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}