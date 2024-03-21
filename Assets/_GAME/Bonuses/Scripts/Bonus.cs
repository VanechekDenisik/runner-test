using System;
using System.Collections.Generic;
using System.Linq;
using Bonuses.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bonuses
{
    [Serializable]
    public struct Bonus
    {
        [HideLabel] public BonusApplier applier;
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

        public float Amount => GetValue(BonusParameterType.Amount);
        public float Duration => GetValue(BonusParameterType.Duration);
        public int Level => (int)GetValue(BonusParameterType.Level);
        
        public Bonus SetAmount(float value) => SetValue(BonusParameterType.Amount, value);
        public Bonus SetDuration(float value) => SetValue(BonusParameterType.Duration, value);
        public Bonus SetLevel(int value) => SetValue(BonusParameterType.Level, value);
        
        public bool HasAmount => HasNonZeroValue(BonusParameterType.Amount);
        public bool HasDuration => GetValue(BonusParameterType.Duration) > 0;
        public bool HasLevel => HasNonZeroValue(BonusParameterType.Duration);

        public string Description
        {
            get
            {
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