namespace ScientificCalculator.API.Utils
{
    public static class Tokenize
    {
        public static List<string> TokenizeExpression(string expression)
        {
            var tokens = new List<string>();
            var currentToken = string.Empty;

            foreach (var ch in expression)
            {
                if (char.IsWhiteSpace(ch))
                {
                    continue;
                }

                if (char.IsDigit(ch) || ch == '.')
                {
                    currentToken += ch;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        tokens.Add(currentToken);
                        currentToken = string.Empty;
                    }

                    tokens.Add(ch.ToString());
                }
            }

            if (!string.IsNullOrEmpty(currentToken))
            {
                tokens.Add(currentToken);
            }

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
            return tokens;
        }
    }
}