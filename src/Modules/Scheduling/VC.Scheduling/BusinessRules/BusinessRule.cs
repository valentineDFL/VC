namespace VC.Scheduling.BusinessRules;

public abstract class BusinessRule
{
    public abstract Task<bool> EvaluateAsync(
        TimeSlot slot, 
        SchedulingContext context,
        CancellationToken ct = default
    );

    protected virtual async Task<bool> EvaluateCoreAsync(
        TimeSlot slot, 
        SchedulingContext context,
        CancellationToken ct)
    {
        // Базовая реализация для переопределения
        return await Task.FromResult(true);
    }

    public BusinessRule And(BusinessRule other) 
        => new CompositeRule(this, other, LogicalOperator.And);

    public BusinessRule Or(BusinessRule other) 
        => new CompositeRule(this, other, LogicalOperator.Or);
}
