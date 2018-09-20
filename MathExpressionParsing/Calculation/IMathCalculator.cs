namespace MathExpressionParsing.Calculation
{
    public interface IMathCalculator
    {
        /// <summary>
        /// Calculate mathematical expressions specified in infix notation.
        /// </summary>
        /// <param name="expression">Mathematical expressions.</param>
        /// <returns>Calculation result.</returns>
        double Calculate(string expression);
    }
}