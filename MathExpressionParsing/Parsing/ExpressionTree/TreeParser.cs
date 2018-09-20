using System;
using System.Collections.Generic;
using MathExpressionParsing.Parsing.ExpressionTree.SyntaxTree;
using MathExpressionParsing.Tokenization;

namespace MathExpressionParsing.Parsing.ExpressionTree
{
    public class TreeParser
    {
        private readonly IEnumerator<Token> _enumerator;

        private Token CurrentToken => _enumerator.Current;

        private void MoveNext()
        {
            _enumerator.MoveNext();
        }

        public TreeParser(IEnumerable<Token> tokens)
        {
            _enumerator = tokens.GetEnumerator();
        } 
        
        public INode ParseExpression()
        {
            // Move parser to first token.
            MoveNext();
            
            // Start expression parsing.
            var expr = ParseAddSubtract();

            // Check that whole expression parsed.
            if (_enumerator.MoveNext())
                throw new SyntaxException($"Unexpected token: {_enumerator.Current.Value}");

            return expr;
        }

        // Parse add/subtract operations
        private INode ParseAddSubtract()
        {
            // Parse the left hand side
            var lhs = ParseMultiplyDivide();

            while (CurrentToken.Type == TokenType.Add || CurrentToken.Type == TokenType.Subtract)
            {
                Func<double, double, double> op;
                if (CurrentToken.Type == TokenType.Add)
                {
                    op = (a, b) => a + b;
                }
                else
                {
                    op = (a, b) => a - b;
                }
          
                // Skip the operator
                MoveNext();
                
                // Parse the right hand side of the expression
                var rhs = ParseMultiplyDivide();
                
                lhs = new BinaryOperationNode(lhs, rhs, op);
            }

            return lhs;
        }

        // Parse multiply/divide operations
        private INode ParseMultiplyDivide()
        {
            // Parse the left hand side
            var lhs = ParseUnary();

            while (CurrentToken.Type == TokenType.Multiply || CurrentToken.Type == TokenType.Divide)
            {
                Func<double, double, double> op;
                if (CurrentToken.Type == TokenType.Multiply)
                {
                    op = (a, b) => a * b;
                }
                else
                {
                    op = (a, b) => a / b;
                }
         
                // Skip the operator
                MoveNext();
                
                // Parse the right hand side of the expression
                var rhs = ParseUnary();
                
                lhs = new BinaryOperationNode(lhs, rhs, op);
            }

            return lhs;
        }


        // Parse unary operators (we can count only negative operations).
        private INode ParseUnary()
        {
            var negativeCount = 0;
            
            while (CurrentToken.Type == TokenType.Add || CurrentToken.Type == TokenType.Subtract)
            {
                if (CurrentToken.Type == TokenType.Subtract)
                {
                    negativeCount++;
                }

                // Skip
                MoveNext();
            }
            
            var node = ParseLeaf();

            if (negativeCount > 0 && negativeCount % 2 != 0)
            {
                node = new UnaryOperationNode(node, n => -n);
            }

            return node;
        }

        // Parse a leaf node
        private INode ParseLeaf()
        {
            INode node;
            
            switch (CurrentToken.Type)
            {
                case TokenType.Number:
                    node = new NumberNode(double.Parse(CurrentToken.Value));

                    MoveNext();
                    
                    break;
                case TokenType.Variable:
                    node = new VariableNode(CurrentToken.Value);

                    MoveNext();
                        
                    break;
                case TokenType.OpenParenthesis:
                    // Skip '('
                    MoveNext();

                    // Parse expression inside the parenthesis.
                    node = ParseAddSubtract();

                    // Check for ')'
                    if (CurrentToken.Type != TokenType.CloseParenthesis)
                        throw new SyntaxException("Missing close parenthesis");
    
                    // Skip ')'
                    MoveNext();
                    
                    break;
                default:
                    throw new SyntaxException($"Unexpected token: {CurrentToken.Value}");
            }

            return node;
        }

    }
}
