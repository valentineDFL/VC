using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;

namespace VC.UnitTests.Common;

/// <summary>
/// Добавляет возможность прокидывать в качестве параметров <see cref="Mock"/>
/// </summary>
public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute(params Type[] customizationType) : 
        base(() => FixtureFactory.Create(customizationType).Customize(new AutoMoqCustomization { ConfigureMembers = true }))
    {
    }
}