using DotNetEnv;
using RivalTranslator.Interfaces;
using RivalTranslator.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Env.Load();

// Add services to the container.
builder.Services.AddRazorPages();

// Dependency injections
builder.Services.AddScoped<ITranslationService, GoogleTranslationService>();
builder.Services.AddScoped<ILanguageDetectionService, GoogleTranslationService>();
builder.Services.AddScoped<ILanguageValidationService, LanguageValidationService>();
builder.Services.AddScoped<ILanguageProviderService, JsonLanguageProviderService>();
builder.Services.AddScoped<ILanguageProviderFactory, LanguageProviderFactory>();
builder.Services.AddScoped<ILoggerService, ConsoleLoggerService>();



var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
