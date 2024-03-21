using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    public abstract class DurationBonusType : AmountBonusType
    {
        public override Bonus Plus(Bonus b1, Bonus b2)
        {
            return new Bonus(b1)
                .SetAmount(Mathf.Max(b1.Amount, b2.Amount))
                .SetDuration(Mathf.Min(b1.Duration + b2.Duration, Mathf.Max(b1.Duration, b2.Duration)));
        }

        public override Bonus Multiply(Bonus b, float c)
        {
            return new Bonus(b).SetDuration(c * b.Duration);
        }

        public override void OnBonusAdded(BonusesController target, Bonus previousBonus,
            Bonus bonusDelta)
        {
            var currentBonus = target.GetComponent<BonusesController>().GetBonus(bonusDelta.applier);
            if (!currentBonus.HasDuration)
            {
                target.UnregisterBonus(currentBonus);
                new Bonus(currentBonus)
                    .SetAmount(-currentBonus.Amount)
                    .ApplyToTarget(target);
            }
            else if (!previousBonus.HasAmount)
            {
                currentBonus.ApplyToTarget(target);
            }
        }
    }
}