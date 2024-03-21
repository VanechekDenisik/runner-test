using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.UI;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : EntityComponentWithConfig<CharacterConfig>
    {
        [SerializeField] private ParameterGameObject tapCatcherParameter;
        
        [InjectFromEntity] private Rigidbody _rigidBody;
        
        public float MovementSpeedPercentageBonus { get; set; }

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

        private float MovementSpeed => Config.MovementSpeed * (100f + MovementSpeedPercentageBonus) / 100f;
        private Vector3 DeltaMovementVector => Time.deltaTime * MovementSpeed * Vector3.forward;

        private void Jump()
        {
            if (!_isOnFloor) return;
            
            _rigidBody.AddForce(Config.JumpImpulse * Vector3.up, ForceMode.VelocityChange);
        }

        private void ApplyGravity()
        {
            _rigidBody.AddForce(Config.Gravity * Time.deltaTime * Vector3.down, ForceMode.Acceleration);
        }
        
        private static bool CollidedWithFloor(Collision other)
        {
            return other.gameObject.CompareTag($"Floor");
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!CollidedWithFloor(other)) return;
            _isOnFloor = true;
        }

        private void OnCollisionExit(Collision other)
        {
            if (!CollidedWithFloor(other)) return;
            _isOnFloor = false;
        }
    }
}