# Math Expression Parsing

This repository compare 2 calculators for calculating math expression.
First one use Shunting-yard algorithm in order to convert Infix notation into postfix notation and evaluate result.
Second one turn input text into parse tree. 

Each calculator implement the same interface and might consume valid and invalid expression.
```csharp
public interface IMathCalculator
{
    // Calculate mathematical expressions specified in infix notation.
    double Calculate(string expression);
}
```
Examples of valid math expressions:
- 2 + -1 = 1
- 2*3 --4 = 10
- ((2 + -1) * (-3 * Pi))/-4 = 2.355

Example of any invalid math expression:
- 2 * * 2  = error
*(two multiplication operators can not go one after another)*
- 1 + = error
*(missied second operand)*     
- (2+1 = error
*(parenthesis missied)*
