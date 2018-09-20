using System.Collections.Generic;
using System.Linq;
using MathExpressionParsing.Tokenization;

namespace MathExpressionParsing.Parsing.ShuntingYardWithRPN
{
    /// <summary>
    /// Parse mathematical expressions specified in infix notation and produce a postfix notation expression.
    /// Use shunting-yard algorithm for parsing, the idea is to convert infix notation to postfix and then evaluate result.
    /// </summary>
    public class InfixNotationParser
    {
        // Dictionary with all operators precedence.
        private static readonly IDictionary<TokenType, int> OperatorsPrecedence = new Dictionary<TokenType, int>
        {
            [TokenType.Add] = 1,
            [TokenType.Subtract] = 1,
            [TokenType.Multiply] = 2,
            [TokenType.Divide] = 2
        };
        
        /// <summary>
        /// Apply shunting-yard algorithm against expression. 
        /// </summary>
        /// <param name="tokens">Expression tokens in infix notation.</param>
        /// <returns>Postfix notation expression.</returns>
        /// <exception cref="SyntaxException">Thrown in case of syntax error in expression.</exception>
        public IEnumerable<Token> Parse(IEnumerable<Token> tokens)
        {
            var result = new List<Token>();
            var stack = new Stack<Token>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        result.Add(token);
                        break;
                    case TokenType.Add:
                    case TokenType.Subtract:
                    case TokenType.Multiply:
                    case TokenType.Divide:
                        // Currently we have only left associative operators.
                        while (stack.Count > 0 && OperatorsPrecedence.ContainsKey(stack.Peek().Type) && OperatorsPrecedence[token.Type] <= OperatorsPrecedence[stack.Peek().Type])
                        {
                            result.Add(stack.Pop());
                        }

                        stack.Push(token);
                        break;                    
                    case TokenType.OpenParenthesis:
                        stack.Push(token);
                        break;
                    case TokenType.CloseParenthesis:
                        while (stack.Peek().Type != TokenType.OpenParenthesis)
                        {
                            result.Add(stack.Pop());
                        }

                        stack.Pop();
                        break;
                    default:
                        throw new SyntaxException($"Shunting Yard fail, unsupported token found. Token type: {token.Type}, value : {token.Value}");
                }
            }

            while (stack.Any())
            {
                result.Add(stack.Pop());
            }

            return result;
        }
    }
}