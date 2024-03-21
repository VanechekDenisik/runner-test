using Core.Common.Entities;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = CharactersAssetsPaths.Assets + "Character")]
    public class CharacterConfig : EntityConfig
    {
        //[field: SerializeField] removes duplication
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float JumpImpulse { get; private set; }
    }
}