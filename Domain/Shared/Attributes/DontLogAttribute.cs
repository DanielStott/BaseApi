namespace Domain.Shared.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class DontLogAttribute : Attribute
    {
    }
}