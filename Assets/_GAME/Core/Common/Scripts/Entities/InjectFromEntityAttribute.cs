using System;

namespace Core.Common.Entities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectFromEntityAttribute : Attribute
    {
    }
}