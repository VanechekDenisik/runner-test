using System;
using Core.Common.Entities;
using Core.Events.Parameters;
using Core.Helpers;
using UnityEngine;

namespace Camera
{
    public class CameraController : EntityComponent
    {
        //ParameterGameObject is used here for prefab modularity without utilizing a service locator or using statics.
        [SerializeField] private ParameterGameObject parameterGameObjectToFollow;

        private Transform _transformToFollow;
        private Vector3 _positionOffsetFromObjectToFollow;

        private void Awake()
        {
            parameterGameObjectToFollow.Subscribe(StartFollowingObject);
        }

        private void OnDestroy()
        {
            parameterGameObjectToFollow.Unsubscribe(StartFollowingObject);
        }

        private void StartFollowingObject(GameObject obj)
        {
            if (obj == null) return;
            _transformToFollow = obj.transform;
            _positionOffsetFromObjectToFollow = transform.position - _transformToFollow.position;
        }

        private void Update()
        {
            MoveCameraWithObjectToFollow();
        }

        private void MoveCameraWithObjectToFollow()
        {
            if (_transformToFollow == null) return;
            
            transform.position = (_transformToFollow.position + _positionOffsetFromObjectToFollow)
                .WithY(transform.position.y);
        }
    }
}