using Core.Common.Entities;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = CharactersAssetsPaths.Assets + "Character")]
    public class CharacterConfig : EntityConfig
    {
        //Using [field: SerializeField] removes duplication. Otherwise you need to create almost identical field + property.
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float JumpImpulse { get; private set; }
        [field: SerializeField] public float FlyUpSpeed { get; private set; }
        [field: SerializeField] public float FlyHeight { get; private set; }

        [SerializeField] private float flySpeed;
        [SerializeField] private float runSpeed;

        //Implementing this method here helps to achieve better encapsulation => easy logic for understanding for humans.
        //I used "Move method to another class" refactoring pattern because there was a smell of "envy method".
        //You can see the changes in commits in git.
        public float Speed(bool isFlying)
        {
            return isFlying ? flySpeed : runSpeed;
        }
    }
}