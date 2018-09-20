namespace MathExpressionParsing.Parsing
{
    public interface IVariableResolver
    {
        double Resolve(string variableName);
    }
}