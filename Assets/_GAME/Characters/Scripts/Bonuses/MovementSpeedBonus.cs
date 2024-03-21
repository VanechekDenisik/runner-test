using Bonuses;
using Bonuses.Configs;
using UnityEngine;

namespace Characters.Bonuses
{
    [CreateAssetMenu(menuName = BonusesAssetsPaths.Appliers + "Movement speed")]
    public class MovementSpeedBonus : BonusApplierFor<CharacterController>
    {
        protected override void Apply(CharacterController target, Bonus bonus)
        {
            target.MovementSpeedPercentageBonus += bonus.Amount;
        }
    }
}