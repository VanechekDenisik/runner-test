using System;

namespace Core.Common.Entities
{
    [Serializable]
    public class DescriptionSection
    {
        public float Priority { get; private set; }
        public string Description { get; private set; }

        public DescriptionSection(string description, float priority = 0)
        {
            Priority = priority;
            Description = description;
        }
    }
}