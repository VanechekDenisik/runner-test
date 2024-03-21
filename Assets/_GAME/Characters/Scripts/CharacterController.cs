using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.UI;
using UnityEngine;

namespace Characters
{
    public class CharacterController : EntityComponentWithConfig<CharacterConfig>
    {
        [SerializeField] private ParameterGameObject tapCatcherParameter;
        
        [InjectFromEntity] private Rigidbody _rigidBody;

        private bool _isOnFloor;
        
        private void Awake()
        {
            tapCatcherParameter.Subscribe(SubscribeToTapOnScreen);
        }

        private void Update()
        {
            MoveForward();
            ApplyGravity();
        }
        
        private void SubscribeToTapOnScreen(GameObject obj)
        {
            obj.GetComponent<SpriteTapCatcher>().OnDown += Jump;
        }

        private void MoveForward()
        {
            _rigidBody.MovePosition(_rigidBody.position + DeltaMovementVector);
        }

        private Vector3 DeltaMovementVector => Time.deltaTime * Config.MovementSpeed * Vector3.forward;

        private void Jump()
        {
            if (!_isOnFloor) return;
            
            _rigidBody.AddForce(Config.JumpImpulse * Vector3.up, ForceMode.VelocityChange);
        }

        private void ApplyGravity()
        {
            _rigidBody.AddForce(Config.Gravity * Time.deltaTime * Vector3.down, ForceMode.Acceleration);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag($"Floor")) return;
            _isOnFloor = true;
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.CompareTag($"Floor")) return;
            _isOnFloor = false;
        }
    }
}