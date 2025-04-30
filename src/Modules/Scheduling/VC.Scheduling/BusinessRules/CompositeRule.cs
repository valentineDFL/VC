namespace VC.Scheduling.BusinessRules;

/// <summary>
/// Композитное правило для AND/OR.
/// </summary>
public class CompositeRule : BusinessRule
{
    private readonly BusinessRule _left;
    private readonly BusinessRule _right;
    private readonly LogicalOperator _operator;

    public CompositeRule(
        BusinessRule left, 
        BusinessRule right, 
        LogicalOperator logicalOperator)
    {
        _left = left;
        _right = right;
        _operator = logicalOperator;
    }

    public override async Task<bool> EvaluateAsync(
        TimeSlot slot, 
        SchedulingContext context,
        CancellationToken ct)
    {
        var leftResult = await _left.EvaluateAsync(slot, context, ct);
        var rightResult = await _right.EvaluateAsync(slot, context, ct);

        return _operator switch
        {
            LogicalOperator.And => leftResult && rightResult,
            LogicalOperator.Or => leftResult || rightResult,
            _ => throw new NotSupportedException()
        };
    }
}