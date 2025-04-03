namespace VC.Recources.Domain.Entities;

public record Experience
{
    public int Years { get; set; }
    public int Months { get; set; }

    public Experience() { }

    public Experience(int years, int months)
    {
        if (years < 0 || months < 0 || years > 11)
            throw new InvalidOperationException("Invalid experiece");

        Years = years;
        Months = months;
    }

}