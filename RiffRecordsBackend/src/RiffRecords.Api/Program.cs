using Microsoft.OpenApi;
using RiffRecords.Application;
using RiffRecords.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer((operation, _, _) =>
    {
        foreach (var parameter in operation.Parameters ?? [])
        {
            if (parameter.Schema is OpenApiSchema { Format: "int32" } schema &&
                schema.Type == (JsonSchemaType.Integer | JsonSchemaType.String))
            {
                schema.Type = JsonSchemaType.Integer;
                schema.Pattern = null;
            }
        }

        return Task.CompletedTask;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "RiffRecords API"));
}

app.MapControllers();
app.Run();