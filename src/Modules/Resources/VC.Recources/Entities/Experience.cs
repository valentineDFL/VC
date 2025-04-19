namespace VC.Recources.Domain.Entities;

public record Experience
{
    public int Years { get; set; }
    public int Months { get; set; }

    public Experience() { }

    public Experience(int years, int months)
    {
        if (years < 0 || months < 0)
            throw new InvalidOperationException("Invalid experience");
        
        Years = years;
        Months = months;
    }
}