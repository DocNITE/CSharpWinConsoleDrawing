using App.Test.Interfaces;

namespace App.Test.GameObjects;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterComponentAttribute : Attribute
{
}