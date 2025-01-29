using DotNetEnv;
using RivalTranslator.Interfaces;
using RivalTranslator.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Env.Load();

// Add services to the container.
builder.Services.AddRazorPages();

// Custom services
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<ILanguageValidationService, LanguageValidationService>();
builder.Services.AddScoped<ILanguageProvider, JsonLanguageProvider>();




var app = builder.Build();

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
