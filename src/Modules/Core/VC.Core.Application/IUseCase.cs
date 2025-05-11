namespace VC.Core.Application;

public interface IUseCase<in TParams>
{
    Task ExecuteAsync(TParams parameters, CancellationToken cancellationToken = default);
}

public interface IUseCase<in TParams, TResult>
{
    Task<TResult> ExecuteAsync(TParams parameters, CancellationToken cancellationToken = default);
}

public class Unit
{
    public static readonly Unit Value = new();
    private Unit() { }
}