using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    public abstract class BonusApplier : ScriptableObject
    {
        [field:SerializeField, TextArea(3, 20)] public string DescriptionText { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public BonusType Type { get; private set; }

        public abstract void Apply(BonusesController target, Bonus bonus);

        public virtual bool IsAvailable(BonusesController target, Bonus bonus)
        {
            return true;
        }
        
        public string Description()
        {
            var description = Type.UpdateDescription(DescriptionText);
            return UpdateDescription(description);
        }

        protected virtual string UpdateDescription(string description)
        {
            return description;
        }

        public virtual bool IsAvailable<T>(T target)
        {
            return true;
        }
    }
}