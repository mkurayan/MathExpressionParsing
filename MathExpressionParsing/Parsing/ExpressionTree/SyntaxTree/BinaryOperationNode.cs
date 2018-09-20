using System;

namespace MathExpressionParsing.Parsing.ExpressionTree.SyntaxTree
{
    public class BinaryOperationNode : INode
    {
        private readonly INode _leftNode;
        private readonly INode _rightNode;
        private readonly Func<double, double, double> _binaryOperation;
        
        public BinaryOperationNode(INode left, INode right, Func<double, double, double> operation)
        {
            _leftNode = left;
            _rightNode = right;
            _binaryOperation = operation;
        }

        public double Evaluate(IVariableResolver resolver)
        {
            return _binaryOperation(_leftNode.Evaluate(resolver), _rightNode.Evaluate(resolver));
        }
    }
}