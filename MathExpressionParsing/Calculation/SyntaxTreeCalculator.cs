using MathExpressionParsing.Parsing;
using MathExpressionParsing.Parsing.ExpressionTree;
using MathExpressionParsing.Tokenization;

namespace MathExpressionParsing.Calculation
{
    public class SyntaxTreeCalculator : IMathCalculator
    {
        private readonly Tokenizer _tokenizer = new Tokenizer();

        private readonly IVariableResolver _variableResolver;
        
        public SyntaxTreeCalculator(IVariableResolver variableResolver)
        {
            _variableResolver = variableResolver;
        }
        
        public double Calculate(string expression)
        {
            // Split expression to tokens. 
            var tokens = _tokenizer.Tokenize(expression);

            if (tokens.Length == 0)
            {
                // Expression empty, nothing to calculate.
                return 0;
            }

            var parser = new TreeParser(tokens);

            return parser.ParseExpression().Evaluate(_variableResolver);
        }
    }
}