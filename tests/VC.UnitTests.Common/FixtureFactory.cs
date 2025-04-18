using AutoFixture;

namespace VC.UnitTests.Common;

public static class FixtureFactory
{
    public static Fixture Create()
    {
        var fixture = new Fixture();
        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        fixture.Customize<TimeOnly>(composer => composer.FromFactory<DateTime>(TimeOnly.FromDateTime));
        
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
    
    public static Fixture Create(params Type[] customizationTypes)
    {
        var fixture = Create();
        foreach (var customizationType in customizationTypes)
        {
            if (Activator.CreateInstance(customizationType) is ICustomization customization)
                fixture.Customize(customization);
            else
                throw new InvalidOperationException($"Тип {customizationType.Name} не является ICustomization");
        }

        return fixture;
    }
}