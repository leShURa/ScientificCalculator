namespace Features.Calcul;

static class Calcul
{
    internal static double CalculTwoTerms(double a, string op, double b)
    {
        return op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => a / b  != 0  ? a / b : throw new DivideByZeroException("Division by zero is not allowed.")     ,
            _ => throw new FormatException("Invalid operator.")
        };
    }
    internal static List<string> CalculateWithPriorities(List<string> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "*" || tokens[i] == "/")
            {
                double left = double.Parse(tokens[i - 1]);
                double right = double.Parse(tokens[i + 1]);
                double result = CalculTwoTerms(left, tokens[i], right);
                tokens[i - 1] = result.ToString();
                tokens.RemoveAt(i); // Remove operator
                tokens.RemoveAt(i); // Remove right operand
                i--; // Move back to the previous token
            }
        }

        // Then, handle addition and subtraction
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "+" || tokens[i] == "-")
            {
                double left = double.Parse(tokens[i - 1]);
                double right = double.Parse(tokens[i + 1]);
                double result = CalculTwoTerms(left, tokens[i], right);
                tokens[i - 1] = result.ToString();
                tokens.RemoveAt(i); // Remove operator
                tokens.RemoveAt(i); // Remove right operand
                i--; // Move back to the previous token
            }
        }

        return tokens;
    }
}