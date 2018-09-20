using MathExpressionParsing.Calculation;
using Xunit;

namespace MathExpressionParsingTests.CalculatorIntegrationTests
{
    public class SypCalculatorTests
    {
        private readonly IMathCalculator _calculator;
        
        public SypCalculatorTests()
        {
            _calculator = new SypCalculator(new MathConstantResolver());
        }
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("11", 11)]
        [InlineData("Pi", 3.14)]
        public void Calculate_SingleNumberAsMathExpression_NumberItself(string mathExpression, double expectedResult)
        {
            Assert.Equal(expectedResult, _calculator.Calculate(mathExpression));
        }

        [Theory]
        [InlineData("1 + 1", 2)]
        [InlineData("2 - 1", 1)]
        [InlineData("3 * 2", 6)]
        [InlineData("6 / 2", 3)]
        [InlineData("Pi * 2", 3.14 * 2)]
        public void Calculate_MathExpressionWithSingleBinaryOperation_CorrectCalculationResult(
            string mathExpression,
            double expectedResult)
        {
            Assert.Equal(expectedResult, _calculator.Calculate(mathExpression));
        }
        
        [Theory]
        [InlineData("1 + 1 + 1", 3)]
        [InlineData("2 - 8 / 4", 0)]
        [InlineData("3 * 2 + 1", 7)]
        [InlineData("6 / 2 * 2", 6)]
        [InlineData("Pi - Pi * 2 ", -3.14)]
        public void Calculate_MathExpressionWithMultipleBinaryOperations_CorrectCalculationResult(
            string mathExpression,
            double expectedResult)
        {
            Assert.Equal(expectedResult, _calculator.Calculate(mathExpression));
        }               
        
        [Theory]
        [InlineData("1 + 1 + 1", 3)]
        [InlineData("(2 - 8) / 2", -3)]
        [InlineData("(1+1) * (3-1)", 4)]
        [InlineData("((1) + (2))/(6/(1+1))", 1)]
        [InlineData("((Pi - Pi) + 2) * 4", 8)]
        public void Calculate_MathExpressionParenthesis_CorrectCalculationResult(
            string mathExpression,
            double expectedResult)
        {
            Assert.Equal(expectedResult, _calculator.Calculate(mathExpression));
        }        
    }
}