using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.Helpers;
using Core.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : EntityComponentWithConfig<CharacterConfig>
    {
        [SerializeField] private ParameterGameObject tapCatcherParameter;
        
        [InjectFromEntity] private Rigidbody _rigidBody;
        
        public float MovementSpeedPercentageBonus { get; set; }

        public float FlyBonus { get; private set; }

        public bool IsFlying => FlyBonus > 0;

        public bool IsOnFloor { get; private set; }

        public event Action OnIsOnFloorChanged;
        public event Action OnIsFlyingChanged;

        public void AddFlyBonus(float delta)
        {
            FlyBonus += delta;
            OnIsFlyingChanged?.Invoke();
        }
        
        private void Awake()
        {
            tapCatcherParameter.Subscribe(SubscribeToTapOnScreen);
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            Move();
        }

        private void Update()
        {
            TryDie();
            
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
        private Vector3 ForwardMovingVector => Time.fixedDeltaTime * MovementSpeed * Vector3.forward;
        private float FlyHeight => Mathf.Lerp(transform.position.y, Config.FlyHeight, Time.deltaTime * Config.FlyUpSpeed);

        private Vector3 NextMovePosition => !IsFlying
            ? _rigidBody.position + ForwardMovingVector
            : (_rigidBody.position + ForwardMovingVector).WithY(FlyHeight);

        private void Jump()
        {
            if (!IsOnFloor) return;
            
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
            IsOnFloor = true;
            OnIsOnFloorChanged?.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {
            if (!CollidedWithFloor(other)) return;
            IsOnFloor = false;
            OnIsOnFloorChanged?.Invoke();
        }

        private void TryDie()
        {
            if (transform.position.y > -5) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}