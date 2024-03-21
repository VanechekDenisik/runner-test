using UnityEngine;

namespace Bonuses.Configs
{
    public abstract class BonusApplierFor<T> : BonusApplier where T : Component
    {
        public override void Apply(BonusesController target, Bonus bonus)
        {
            var component = target.GetComponent<T>();
            if (component == null) return;
            Apply(component, bonus);
        }
        
        public override bool IsAvailable(BonusesController target, Bonus bonus)
        {
            var component = target.GetComponent<T>();
            if (component == null) return false;
            return IsAvailable(component);
        }
        
        protected abstract void Apply(T target, Bonus bonus);

        protected virtual bool IsAvailable(T target)
        {
            return true;
        }
    }
}