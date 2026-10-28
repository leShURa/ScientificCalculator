internal interface ICalculator
{
    public string CalculateExpression(string expression);
    public bool CreateFunction(string name, string[] parameters, string formula);
}