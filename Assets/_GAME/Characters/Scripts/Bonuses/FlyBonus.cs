using Bonuses;
using Bonuses.Configs;
using UnityEngine;

namespace Characters.Bonuses
{
    [CreateAssetMenu(menuName = BonusesAssetsPaths.Appliers + "Fly")]
    public class FlyBonus : BonusApplierFor<CharacterController>
    {
        protected override void Apply(CharacterController target, Bonus bonus)
        {
            target.FlyBonus += bonus.Amount;
        }
    }
}