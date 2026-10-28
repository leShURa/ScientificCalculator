using Handler.Functionhandler;
using Features.Calcul;
using ScientificCalculator.API.Utils;
using Handler.ParenthesisHandler;

class Calculator : ICalculator
{
    private FunctionHandler function_handler { get; set; }
    private ParenthesisHandler parenthesis_handler { get; set; }

    public Calculator()
    {
        function_handler = new();
        parenthesis_handler = new();
    }
    public async Task<string> CalculateExpression(string expression)
    {
        List<string> tokens = Tokenize.TokenizeExpression(expression);

        List<string> token_without_functions = await function_handler.FunctionParsing(tokens);
        List<string> tokens_without_parenthesis = parenthesis_handler.CalculateWithParenthesis(tokens_without_parenthesis)
    }
    public bool CreateFunction(string name, string[] parameters, string formula)
    {
        
    }
}