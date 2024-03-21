using Bonuses.Configs;
using UnityEngine;

namespace Bonuses.Appliers
{
    [CreateAssetMenu(menuName = BonusesAssetsPaths.Appliers + "Pack")]
    public class PackOfBonuses : BonusApplier
    {
        [SerializeField] private Bonus[] bonuses;
        
        public override void Apply(BonusesController target, Bonus bonus)
        {
            target.AddBonuses(bonuses);
        }
    }
}