using AutoFixture.Xunit2;

namespace VC.UnitTests.Common;

/// <summary>
/// Расширяет <see cref="AutoMoqDataAttribute"/> добавляя возможность использовать поведение <see cref="InlineDataAttribute"/>
/// </summary>
public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
    { }
    public InlineAutoMoqDataAttribute(Type[] customizations, params object[] values)
         : base(new AutoMoqDataAttribute(customizations ?? []), values)
    { }
}