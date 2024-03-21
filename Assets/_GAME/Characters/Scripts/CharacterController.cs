using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.Helpers;
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
        public float FlyBonus { get; set; }

        public bool IsFlying => FlyBonus > 0;

        private bool _isOnFloor;
        
        private void Awake()
        {
            tapCatcherParameter.Subscribe(SubscribeToTapOnScreen);
        }

        private void Update()
        {
            Move();
            ApplyGravity();
            
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }
        
        private void SubscribeToTapOnScreen(GameObject obj)
        {
            obj.GetComponent<SpriteTapCatcher>().OnDown += Jump;
        }

        private void Move()
        {
            _rigidBody.MovePosition(NextMovePosition);
        }

        private float MovementSpeed => (IsFlying ? Config.FlySpeed : Config.RunSpeed) * (100f + MovementSpeedPercentageBonus) / 100f;
        private Vector3 ForwardMovingVector => Time.deltaTime * MovementSpeed * Vector3.forward;
        private float FlyHeight => Mathf.Lerp(_rigidBody.position.y, Config.FlyHeight, Time.deltaTime * Config.FlyUpSpeed);

        private Vector3 NextMovePosition => !IsFlying
            ? _rigidBody.position + ForwardMovingVector
            : (_rigidBody.position + ForwardMovingVector).WithY(FlyHeight);

        private void Jump()
        {
            if (!_isOnFloor) return;
            
            _rigidBody.AddForce(Config.JumpImpulse * Vector3.up, ForceMode.VelocityChange);
        }

        private void ApplyGravity()
        {
            if (IsFlying) return;
            
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