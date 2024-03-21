using System;
using Core.Common.Entities;
using DG.Tweening;
using UnityEngine;

namespace Collectables
{
    public class Collectable : EntityComponentWithConfig<CollectableConfig>
    {
        private void Awake()
        {
            if (Config.RotationSpeed > 0)
            {
                transform.DOLocalRotate(new Vector3(0, 360, 0), 1 / Config.RotationSpeed, RotateMode.LocalAxisAdd)
                    .SetLoops(-1)
                    .SetEase(Ease.Linear);
            }
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"Character")) return;
            
            foreach (var bonus in Config.Bonuses)
                bonus.AddToTarget(other.gameObject);
            
            gameObject.SetActive(false);
        }
    }
}