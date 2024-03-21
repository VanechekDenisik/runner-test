using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    public abstract class BonusType : ScriptableObject
    {
        public abstract Bonus Plus(Bonus b1, Bonus b2);
        public abstract Bonus Multiply(Bonus b, float c);

        public abstract void OnBonusAdded(BonusesController target, Bonus previousBonus,
            Bonus bonusDelta);

        public virtual string UpdateDescription(string description)
        {
            return description;
        }
    }
}