using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    //These class helps to create bonuses with different mechanics. If you require bonus that lasts for some levels or
    //bonus that have stacks then you should inherit from that class.
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