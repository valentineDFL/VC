using AutoFixture;

namespace VC.UnitTests.Common;

public class BaseUnitTest
{
    protected virtual Fixture Fixture => FixtureFactory.Create();
}