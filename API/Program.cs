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


app.MapGet("/calculator/{expression}", (string expression) =>
{
    try
    {
        
    }
    catch (FormatException)
    {
        return Results.BadRequest("Invalid expression format.");
    }
    catch (DivideByZeroException)
    {
        return Results.BadRequest("Division by zero is not allowed.");
    }
    catch 
})
.WithName("GetWeatherForecast");


app.Run("https://localhost:5000");


