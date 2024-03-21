using System;
using Core.Helpers;
using Sirenix.OdinInspector;

namespace Bonuses
{
    [Serializable]
    public struct BonusParameter
    {
        [HorizontalGroup, HideLabel] public BonusParameterType type;
        [HorizontalGroup(Width = 100), HideLabel] public float value;

        public BonusParameter(BonusParameterType type, float value)
        {
            this.type = type;
            this.value = value;
        }

        public string UpdateDescription(string description)
        {
            return description.SafeReplace("{" + type.ToString("F").ToLower() + "}", value.ToString("R"));
        }
    }
}