using System;
using System.Collections.Generic;
using System.Linq;
using Bonuses.Configs;
using Core.Common.Entities;
using Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bonuses
{
    public class BonusesController : EntityComponentWithConfig<IBonusesConfig>
    {
        [SerializeField] [ReadOnly] private List<Bonus> bonuses = new();

        private void Awake()
        {
            if (Config != null) AddBonuses(Config.Bonuses);
        }

        private void Update()
        {
            UpdateDurationOfBonuses<SecondsDurationBonusType>(-Time.deltaTime);
        }

        public void UpdateDurationOfBonuses<T>(float deltaDuration) where T : DurationBonusType
        {
            var deltaBonuses = new List<Bonus>();
            foreach (var bonus in bonuses.ToList())
            {
                if (bonus.applier.Type is not T) continue;
                deltaBonuses.Add(bonus.SetDuration(deltaDuration));
            }
            AddBonuses(deltaBonuses.ToArray());
        }
        
        public event Action OnBonusesChanged;

        //This method should be refactored. It is quite big.
        public void AddBonuses(params Bonus[] deltaBonuses)
        {
            if (deltaBonuses.IsEmpty()) return;

            var previousBonuses = bonuses.ToArray();

            foreach (var bonus in deltaBonuses)
            {
                var id = bonuses.FindIndex(b => b == bonus);
                if (id == -1) bonuses.Add(bonus);
                else bonuses[id] += bonus;
            }

            foreach (var deltaBonus in deltaBonuses)
            {
                var previousBonus = previousBonuses.FirstOrDefault(b => b == deltaBonus);
                deltaBonus.applier.Type.OnBonusAdded(this, previousBonus, deltaBonus);
            }

            OnBonusesChanged?.Invoke();
        }

        //And lower ones are in contrary are quite good!
        public float GetAmount<T>() where T : BonusApplier
        {
            return GetBonuses().Sum(b => b.Amount);
        }

        public float GetAmount<T>(Func<T, bool> filter) where T : BonusApplier
        {
            return GetBonuses(filter).Sum(b => b.Amount);
        }

        public Bonus[] GetBonuses()
        {
            return bonuses.ToArray();
        }

        public Bonus GetBonus(BonusApplier applier)
        {
            return bonuses.FirstOrDefault(b => b.applier == applier);
        }

        public Bonus GetBonus<T>() where T : BonusApplier
        {
            return bonuses.FirstOrDefault(b => b.applier is T);
        }

        public Bonus[] GetBonuses<T>() where T : BonusApplier
        {
            return bonuses.Where(b => b.applier is T).ToArray();
        }

        public Bonus GetBonus<T>(Func<T, bool> filter) where T : BonusApplier
        {
            return bonuses.FirstOrDefault(b => b.applier is T type && filter(type));
        }

        public Bonus[] GetBonuses<T>(Func<T, bool> filter) where T : BonusApplier
        {
            return bonuses.Where(b => b.applier is T type && filter(type)).ToArray();
        }

        internal void UnregisterBonus(Bonus bonus)
        {
            bonuses.Remove(bonus);
        }
    }
}