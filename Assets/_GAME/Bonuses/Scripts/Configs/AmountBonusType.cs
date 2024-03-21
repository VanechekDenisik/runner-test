using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    [CreateAssetMenu(menuName = BonusesAssetsPaths.Types + "Amount", order = -1)]
    public class AmountBonusType : BonusType
    {
        public override Bonus Plus(Bonus b1, Bonus b2)
        {
            return new Bonus(b1).SetAmount(b1.Amount + b2.Amount);
        }

        public override Bonus Multiply(Bonus b, float c)
        {
            return new Bonus(b).SetAmount(c * b.Amount);
        }
        
        public override void OnBonusAdded(BonusesController target, Bonus previousBonus,
            Bonus bonusDelta)
        {
            var currentBonus = target.GetBonus(bonusDelta.applier);
            if (currentBonus.Amount == 0) target.UnregisterBonus(currentBonus);
            bonusDelta.ApplyToTarget(target);
        }
    }
}