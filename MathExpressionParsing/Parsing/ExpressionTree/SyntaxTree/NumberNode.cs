namespace MathExpressionParsing.Parsing.ExpressionTree.SyntaxTree
{
    public class NumberNode : INode
    {
        private readonly double _nodeValue;

        public NumberNode(double value)
        {
            _nodeValue = value;
        }

        public double Evaluate(IVariableResolver resolver)
        {
            return _nodeValue;
        }
    }
}