namespace VC.Recources.Domain;

public interface INameUniquenessChecker
{
    Task<bool> IsNameUniqueAsync(string name, Guid currentId);
}