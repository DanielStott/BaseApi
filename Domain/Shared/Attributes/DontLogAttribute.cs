using System;

namespace Domain.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DontLogAttribute : Attribute
{
}