using Core.Helpers;
using UnityEngine;

namespace Bonuses.Configs
{
    //These class helps to create bonuses that applies to different classes. If you need to apply bonus for movement
    //or for money income or for damage for unit's auto attacks then you should inherit from the class. Also
    //check BonusApplierFor class - it is quite convenient.
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