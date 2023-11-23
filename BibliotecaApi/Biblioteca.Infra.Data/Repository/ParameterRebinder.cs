using System.Linq.Expressions;

#pragma warning disable CA1050 // Declarar tipos em namespaces
public class ParameterRebinder : ExpressionVisitor
#pragma warning restore CA1050 // Declarar tipos em namespaces
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> map;
    public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
    {
        this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }
    public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
    {
        return new ParameterRebinder(map).Visit(exp);
    }
    protected override Expression VisitParameter(ParameterExpression p)
    {
        if (map.TryGetValue(p, out ParameterExpression replacement))
        {
            p = replacement;
        }
        return base.VisitParameter(p);
    }
}