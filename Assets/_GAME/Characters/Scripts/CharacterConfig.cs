using Core.Common.Entities;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = CharactersAssetsPaths.Assets + "Character")]
    public class CharacterConfig : EntityConfig
    {
        //[field: SerializeField] removes duplication
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float JumpImpulse { get; private set; }
        [field: SerializeField] public float FlySpeed { get; private set; }
        [field: SerializeField] public float FlyHeight { get; private set; }
        [field: SerializeField] public float FlyUpSpeed { get; private set; }
    }
}