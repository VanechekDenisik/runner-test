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
            
            AddBonusesToTarget(other);
            
            gameObject.SetActive(false);
        }

        //I am using "Extract method" refactoring pattern here instead of creating comments. It reduces the amount
        //of information that the reader should understand. Other benefit is that this small method can be used in
        //future without additional refactoring.
        private void AddBonusesToTarget(Collider other)
        {
            foreach (var bonus in Config.Bonuses)
                bonus.AddToTarget(other.gameObject);
        }
    }
}