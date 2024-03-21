using System;
using System.Collections.Generic;
using System.Linq;
using Bonuses.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bonuses
{
    /// <summary>
    /// The struct for bonuses. A bunch of iterations with refactoring was required to achieve such convenient and
    /// easy to understand logic. Such logic can be created only during attentive and diligent every day work on the code.
    /// </summary>
    [Serializable]
    public struct Bonus
    {
        //I am using polymorphism or "Strategy" pattern here.
        [HideLabel] public BonusApplier applier;
        
        //This is the example of "Component" pattern or flexible data structure usage.
        public BonusParameter[] parameters;
        
        public Bonus(Bonus bonus)
        {
            applier = bonus.applier;
            parameters = bonus.parameters;
        }

        public Bonus(BonusApplier applier)
        {
            this.applier = applier;
            this.parameters = Array.Empty<BonusParameter>();
        }
        
        public Bonus(BonusApplier applier, BonusParameter[] parameters)
        {
            this.applier = applier;
            this.parameters = parameters;
        }
        
        public bool HasNonZeroValue(BonusParameterType type)
        {
            return GetValue(type) != 0;
        }

        public float GetValue(BonusParameterType type)
        {
            return parameters?.FirstOrDefault(p => p.type == type).value ?? 0;
        }

        public Bonus SetParameter(BonusParameter parameter)
        {
            return SetValue(parameter.type, parameter.value);
        }
        
        //This method is not quite long but look how it is difficult to understand in comparison with upper one.
        //That's why it should be refactored using "Extract method" refactoring pattern.
        public Bonus SetValue(BonusParameterType type, float value)
        {
            var newParameters = new List<BonusParameter>(parameters);
            var id = newParameters.FindIndex(p => p.type == type);
            if (id == -1)
                newParameters.Add(new BonusParameter(type, value));
            else
                newParameters[id] = new BonusParameter(type, value);
            return new Bonus(applier, newParameters.ToArray());
        }
        
        public Bonus AddValue(BonusParameterType type, float value)
        {
            return SetValue(type, GetValue(type) + value);
        }

        //These methods were introduced to remove duplication in code because they were invoked too often.
        public float Amount => GetValue(BonusParameterType.Amount);
        public float Duration => GetValue(BonusParameterType.Duration);
        public int Level => (int)GetValue(BonusParameterType.Level);
        
        //These methods were introduced for the same reason as above.
        public Bonus SetAmount(float value) => SetValue(BonusParameterType.Amount, value);
        public Bonus SetDuration(float value) => SetValue(BonusParameterType.Duration, value);
        public Bonus SetLevel(int value) => SetValue(BonusParameterType.Level, value);
        
        //And these.
        public bool HasAmount => HasNonZeroValue(BonusParameterType.Amount);
        public bool HasDuration => GetValue(BonusParameterType.Duration) > 0;
        public bool HasLevel => HasNonZeroValue(BonusParameterType.Duration);

        public string Description
        {
            get
            {
                //This is the example of polymorphism.
                var result = applier?.Description();
                foreach (var parameter in parameters)
                    result = parameter.UpdateDescription(result);
                return result;
            }
        }

        public void AddToTarget(GameObject target)
        {
            target.GetComponent<BonusesController>().AddBonuses(this);
        }
        
        internal void ApplyToTarget(GameObject target)
        {
            applier.Apply(target.GetComponent<BonusesController>(), this);
        }

        internal void ApplyToTarget(BonusesController target)
        {
            applier.Apply(target, this);
        }

        public static bool operator ==(Bonus b1, Bonus b2)
        {
            return b1.applier == b2.applier && b1.Level == b2.Level;
        }

        public static bool operator !=(Bonus b1, Bonus b2)
        {
            return !(b1 == b2);
        }

        public static Bonus operator +(Bonus b1, Bonus b2)
        {
            if (b1 != b2) throw new ArgumentException();
            return b1.applier.Type.Plus(b1, b2);
        }

        public static Bonus operator *(float c, Bonus b)
        {
            return b.applier.Type.Multiply(b, c);
        }

        public static Bonus operator *(Bonus b, float c)
        {
            return c * b;
        }

        public static Bonus operator -(Bonus b1, Bonus b2)
        {
            return b1 + -1 * b2;
        }

        public static Bonus operator -(Bonus b)
        {
            return -1 * b;
        }
    }
}