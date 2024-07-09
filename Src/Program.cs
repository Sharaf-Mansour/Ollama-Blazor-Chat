using OllamaLocalChat.Components;
using OllamaLocalChat.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .Services.AddSingleton<AIModel>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error", createScopeForErrors: true)
        .UseHsts();

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
