var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IChatService, AzureOpenAIChatService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", builder =>
    {
        builder.WithOrigins("https://news-article-ui-crg0gng4hhf2e0ad.centralindia-01.azurewebsites.net")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");

app.MapControllers();
app.Run();


