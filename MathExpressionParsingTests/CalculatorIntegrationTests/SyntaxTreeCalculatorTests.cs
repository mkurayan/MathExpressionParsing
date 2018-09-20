using MathExpressionParsing.Calculation;
using Xunit;

namespace MathExpressionParsingTests.CalculatorIntegrationTests
{
    public class SyntaxTreeCalculatorTests : BaseCalculatorTests
    {
        private readonly SyntaxTreeCalculator _syntaxTreeCalculator;
        
        protected override IMathCalculator Calculator => _syntaxTreeCalculator;

        public SyntaxTreeCalculatorTests()
        {
            _syntaxTreeCalculator = new SyntaxTreeCalculator(new MathConstantResolver());
        }

        [Fact]
        public void CheckType()
        {
            Assert.IsType<SyntaxTreeCalculator>(Calculator);
        }
    }
}
