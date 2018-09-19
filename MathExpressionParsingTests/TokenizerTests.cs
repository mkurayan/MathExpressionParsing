using MathExpressionParsing;
using MathExpressionParsing.Tokenization;
using System.Linq;
using Xunit;

namespace MathExpressionParsingTests
{
    public class TokenizerTests
    {
        private Tokenizer Tokenizer { get; }

        public TokenizerTests()
        {
            Tokenizer = new Tokenizer();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void Tokenize_EmptyString_EmptyTokensList(string value)
        {
            Assert.Empty(Tokenizer.Tokenize(value));
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("    ", 0)]
        [InlineData("1", 1)]
        [InlineData("11", 1)]
        [InlineData("pi", 1)]
        [InlineData("1 + 1", 3)]
        [InlineData("x1 + x2", 3)]
        [InlineData("   1    +    1   ", 3)]
        [InlineData("( 1 + pi ) / 2", 7)]
        public void Tokenize_ExpressionWithSeveralTokens_CorrectTokensCount(string value, int expectedTokenCount)
        {
            var tokens = Tokenizer.Tokenize(value);

            Assert.Equal(expectedTokenCount, tokens.Length);
        }

        [Theory]
        [InlineData("?")]
        [InlineData("!")]
        [InlineData("%")]
        [InlineData("&")]
        [InlineData("[")]
        [InlineData("]")]
        [InlineData("{")]
        [InlineData("}")]
        [InlineData("(A?)")]
        [InlineData("x.x")]
        [InlineData("x1.x")]
        [InlineData("1+!")]
        [InlineData("1+?")]
        [InlineData("(1+2)/%")]
        [InlineData("A.A")]
        [InlineData(".")]
        [InlineData(".A")]
        [InlineData(".A1")]
        [InlineData(".(")]
        [InlineData(".+")]
        [InlineData("(.)")]
        [InlineData("(.a)")]
        [InlineData("(.a1)")]
        public void Tokenize_ExpressionContainsInvalidSymbols_ThrowSyntaxException(string value)
        {
            Assert.Throws<SyntaxException>(() => Tokenizer.Tokenize(value));
        }

        [Theory]
        [InlineData("1 + 1 - 1 * 1 / 1", 0)]
        [InlineData("44", 0)]
        [InlineData("pi", 1)]
        [InlineData("myNumber", 1)]
        [InlineData("ABC", 1)]
        [InlineData("1 + A", 1)]
        [InlineData("9 / A", 1)]
        [InlineData("(A)", 1)]
        [InlineData("(A + B) / 3", 2)]
        [InlineData("9 / (A + b)", 2)]
        [InlineData("first second", 2)]
        [InlineData("2 * pi * r", 2)]
        [InlineData("E + x1 + x2", 3)]
        public void Tokenize_MathExpression_ContainsExpectedVariablesCount(string value, int expectedCount)
        {
            var tokens = Tokenizer.Tokenize(value);

            Assert.Equal(expectedCount, tokens.Count(token => token.Type == TokenType.Variable));
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("A", 0)]
        [InlineData("A+B", 0)]
        [InlineData("1", 1)]
        [InlineData("22", 1)]
        [InlineData("123456789", 1)]
        [InlineData("0.55", 1)]
        [InlineData("-0.55", 1)]
        [InlineData("-123456789", 1)]
        [InlineData("1+1", 2)]
        [InlineData("1 + 1 - 1 * 1 / 1", 5)]
        [InlineData("A1 + 1", 1)]
        [InlineData("1 + A", 1)]
        [InlineData("-1 - -A", 1)]
        [InlineData("9 / (1 + b)", 2)]
        [InlineData("9 / (a + b)", 1)]
        public void Tokenize_MathExpression_ContainsExpectedTokensWithNumber(string value, int expectedCount)
        {
            var tokens = Tokenizer.Tokenize(value);

            Assert.Equal(expectedCount, tokens.Count(token => token.Type == TokenType.Number));
        }

        [Theory]
        [InlineData("+", TokenType.Add)]
        [InlineData("-", TokenType.Subtract)]
        [InlineData("*", TokenType.Multiply)]
        [InlineData("/", TokenType.Divide)]
        [InlineData("(", TokenType.OpenParenthesis)]
        [InlineData(")", TokenType.CloseParenthesis)]
        public void CreateNewCellToken_SingleSymbol_ValidToken(string value, TokenType expectedType)
        {
            Assert.Equal(expectedType, Tokenizer.Tokenize(value).First().Type);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("A", 0)]
        [InlineData("A+11", 0)]
        [InlineData("0.55", 0)]
        [InlineData("1 + 1 - 1 * 1 / 1", 0)]
        [InlineData("(A)", 1)]
        [InlineData("(1) + (A)", 2)]
        [InlineData("((1) - (A))", 3)]
        [InlineData("((1) - (A))/(4 * (z+f))", 5)]
        public void Tokenize_MathExpression_ContainsExpectedParenthesisNumber(string value, int expectedCount)
        {
            var tokens = Tokenizer.Tokenize(value);

            Assert.Equal(expectedCount, tokens.Count(token => token.Type == TokenType.OpenParenthesis));
            Assert.Equal(expectedCount, tokens.Count(token => token.Type == TokenType.CloseParenthesis));
        }
    }
}
