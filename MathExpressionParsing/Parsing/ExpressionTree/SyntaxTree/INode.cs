namespace MathExpressionParsing.Parsing.ExpressionTree.SyntaxTree
{
    public interface INode
    {
        double Evaluate(IVariableResolver resolver);
    }
}
