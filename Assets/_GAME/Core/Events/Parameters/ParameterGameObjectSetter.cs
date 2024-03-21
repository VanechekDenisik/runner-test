using System;
using UnityEngine;

namespace Core.Events.Parameters
{
    public class ParameterGameObjectSetter : MonoBehaviour
    {
        [SerializeField] private ParameterGameObject parameter;

        private void Awake()
        {
            parameter.Value = gameObject;
        }
    }
}