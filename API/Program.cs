var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapPost("/api/create_function", async (FunctionRequest requestBody) =>
{
    
}).WithName("CreateFunction");

app.MapPost("/api/calculate_expression", async (string expression) =>
{
    
}).WithName("CalculateExpression");
app.Run("https://localhost:5000");

