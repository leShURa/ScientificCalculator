using Calculator.Features;

namespace Handler.ParenthesisHandler;

class ParenthesisHandler
{
    internal List<string> CalculateWithParenthesis(string expression)
{
    List<string> tokens = expression
        .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
        .ToList();

    int openIndex = tokens.LastIndexOf("(");

    // Aucune parenthèse ouvrante : l'expression est prête à être calculée.
    if (openIndex == -1)
    {
        if (tokens.Contains(")"))
            throw new FormatException("Parenthèses non équilibrées.");

        return tokens;
    }

    int closeIndex = tokens.IndexOf(")", openIndex);

    if (closeIndex == -1)
        throw new FormatException("Parenthèse fermante manquante.");

    // Calcule récursivement le contenu de la parenthèse.
    List<string> subExpressionTokens = tokens.GetRange(
        openIndex + 1,
        closeIndex - openIndex - 1);

    List<string> subResult = CalculateWithParenthesis(
        string.Join(" ", subExpressionTokens));

    // Remplace la parenthèse calculée par son résultat.
    tokens[openIndex] = subResult[0];
    tokens.RemoveRange(openIndex + 1, closeIndex - openIndex);

    // Recommence récursivement avec l'expression mise à jour.
    return CalculateWithParenthesis(string.Join(" ", tokens));
}
}
