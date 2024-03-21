using UnityEngine;

namespace Bonuses.Configs
{
    [CreateAssetMenu(menuName = BonusesAssetsPaths.Types + "Simple", order = -2)]
    public class SimpleBonusType : BonusType
    {
        public override Bonus Plus(Bonus b1, Bonus b2)
        {
            return b1;
        }

        public override Bonus Multiply(Bonus b, float c)
        {
            return b;
        }

        public override void OnBonusAdded(BonusesController target, Bonus previousBonus,
            Bonus bonusDelta)
        {
            bonusDelta.ApplyToTarget(target);
        }
    }
}