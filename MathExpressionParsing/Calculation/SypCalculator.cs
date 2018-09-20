using MathExpressionParsing.Parsing;
using MathExpressionParsing.Parsing.ShuntingYardWithRPN;
using MathExpressionParsing.Tokenization;

namespace MathExpressionParsing.Calculation
{
    /// <summary>
    /// Use shunting-yard algorithm for parsing mathematical expressions specified in infix notation.
    /// Produce a postfix notation which evaluated then. 
    /// </summary>
    public class SypCalculator : IMathCalculator
    {
        private readonly Tokenizer _tokenizer = new Tokenizer();
        
        private readonly InfixNotationParser _infixNotationParser = new InfixNotationParser();

        private readonly PostfixNotationEvaluator _postfixNotationEvaluator;

        public SypCalculator(IVariableResolver variableResolver)
        {
            _postfixNotationEvaluator = new PostfixNotationEvaluator(variableResolver);
        }

        public double Calculate(string expression)
        {
            // Split expression to tokens. 
            var tokens = _tokenizer.Tokenize(expression);

            // Use shunting-yard algorithm for parsing. 
            var parsedTokens = _infixNotationParser.Parse(tokens);

            return _postfixNotationEvaluator.Evaluate(parsedTokens);
        }
    }
}