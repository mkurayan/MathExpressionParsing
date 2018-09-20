namespace MathExpressionParsing.Parsing.ExpressionTree.SyntaxTree
{
    public class VariableNode: INode
    {
        private readonly string _variableName;

        public VariableNode(string variableName)
        {
            _variableName = variableName;
        }

        public double Evaluate(IVariableResolver resolver)
        {
            return resolver.Resolve(_variableName);
        }    
    }
}