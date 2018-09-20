using System;
using System.Collections.Generic;
using MathExpressionParsing.Tokenization;

namespace MathExpressionParsing.Parsing.ShuntingYardWithRPN
{
    /// <summary>
    /// Evaluate postfix notation (Reverse Polish notation)
    /// </summary>
    public class PostfixNotationEvaluator
    {
        private readonly IVariableResolver _variableResolver;
        
        public PostfixNotationEvaluator(IVariableResolver variableResolver)
        {
            _variableResolver = variableResolver ?? throw new ArgumentNullException(nameof(variableResolver));
        }

        /// <summary>
        /// Evaluate postfix expression and return result.  
        /// </summary>
        /// <param name="tokens">Expression tokens in postfix notation.</param>
        /// <returns>Result of expression evaluation.</returns>
        /// <exception cref="SyntaxException">Thrown in case of syntax error in expression.</exception>
        public double Evaluate(IEnumerable<Token> tokens)
        {
            var stack = new Stack<double>();
            
            
            foreach (var token in tokens)
            {
                double rightOperand;
                switch (token.Type)
                {
                    case TokenType.Variable:
                        stack.Push(_variableResolver.Resolve(token.Value));
                        break;
                    case TokenType.Number:
                        stack.Push(double.Parse(token.Value));
                        break;
                    case TokenType.Add:
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case TokenType.Subtract:
                        rightOperand = stack.Pop();
                        stack.Push(stack.Pop() - rightOperand);
                        break;
                    case TokenType.Multiply:
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case TokenType.Divide:
                        rightOperand = stack.Pop();
                        stack.Push(stack.Pop() / rightOperand);
                        break;
                    default:
                        throw new SyntaxException($"RPN evaluation fail, unsupported token found. Token type: {token.Type}, value : {token.Value}");
                }
            }

            if (stack.Count != 1)
            {
                throw new SyntaxException($"Calculation fail, expected single result, but was: {stack.Count}");
            }

            return stack.Pop();
        }
    }
}