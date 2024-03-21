using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.Helpers;
using UnityEngine;

namespace Camera
{
    public class CameraController : EntityComponent
    {
        [SerializeField] private ParameterGameObject parameterGameObjectToFollow;

        private Transform _transformToFollow;
        private Vector3 _positionOffsetFromObjectToFollow;

        private void Awake()
        {
            parameterGameObjectToFollow.Subscribe(StartFollowingObject);
        }

        private void StartFollowingObject(GameObject obj)
        {
            _transformToFollow = obj.transform;
            _positionOffsetFromObjectToFollow = transform.position - _transformToFollow.position;
        }

        private void Update()
        {
            MoveCameraWithObjectToFollow();
        }

        private void MoveCameraWithObjectToFollow()
        {
            transform.position = (_transformToFollow.position + _positionOffsetFromObjectToFollow)
                .WithY(transform.position.y);
        }
    }
}