using System;
using System.Collections;
using System.Collections.Generic;
using Core.Common.Initializers;
using UnityEngine;

namespace Core.Core
{
    public class GameSceneRoot : MonoBehaviour
    {
        [SerializeField] private List<InitializationLayer> initializationLayers;

        private IEnumerator Start()
        {
            foreach (var layer in initializationLayers)
            {
                var startInitializationTime = DateTime.Now;
                var warningDelivered = false;

                layer.Initialize();

                while (!layer.IsInitialized)
                {
                    yield return null;

                    if (!warningDelivered && startInitializationTime.AddSeconds(10) < DateTime.Now)
                    {
                        warningDelivered = true;
                        Debug.LogError(
                            $"Layer {layer.Layer} is initializing too long, maybe you forgot invoke OnInitializationComplete() in any of your initialization objects?");
                    }
                }
            }
        }
    }
}