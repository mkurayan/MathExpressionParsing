using MathExpressionParsing.Calculation;
using Xunit;

namespace MathExpressionParsingTests.CalculatorIntegrationTests
{
    public class ShuntingYardCalculatorTests : BaseCalculatorTests
    {
        private readonly ShuntingYardCalculator _shuntingYardCalculator;
        
        protected override IMathCalculator Calculator => _shuntingYardCalculator;

        public ShuntingYardCalculatorTests()
        {
            _shuntingYardCalculator = new ShuntingYardCalculator(new MathConstantResolver());
        }

        [Fact]
        public void CheckType()
        {
            Assert.IsType<ShuntingYardCalculator>(Calculator);
        }
    }
}
