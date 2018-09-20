using MathExpressionParsing;
using MathExpressionParsing.Parsing;

namespace MathExpressionParsingTests.CalculatorIntegrationTests
{
    /// <summary>
    /// Simple Mathematical constant resolver for demo purpose.
    /// </summary>
    public class MathConstantResolver : IVariableResolver
    {
        private const string Pi = "Pi";
        
        public double Resolve(string variableName)
        {
            if (variableName == Pi)
            {
                return 3.14;
            }
                
            throw new SyntaxException($"Unknown variable: {variableName}");
        }
    }
}