using System;
using Core.Common.Entities;
using UnityEngine;

namespace Characters
{
    public class CharacterAnimationsController : EntityComponent
    {
        [InjectFromEntity] private CharacterController _characterController;
        [InjectFromEntity] private Animator _animator;
        
        private static readonly int IsOnFloorParameterHash = Animator.StringToHash("Is on floor");
        private static readonly int IsFlyingParameterHash = Animator.StringToHash("Is flying");

        private void Awake()
        {
            _characterController.OnIsOnFloorChanged += UpdateAnimatorParameters;
            _characterController.OnIsFlyingChanged += UpdateAnimatorParameters;
        }

        private void UpdateAnimatorParameters()
        {
            _animator.SetBool(IsOnFloorParameterHash, _characterController.IsOnFloor);
            _animator.SetBool(IsFlyingParameterHash, _characterController.IsFlying);
        }
    }
}