
using Minikanban.Components;
using Minikanban.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrera delat state som singleton
builder.Services.AddSingleton<KanbanState>();
// Identity-light: simulerad användare/roll i UI
builder.Services.AddSingleton<UserState>();

// Razor Components (Blazor Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
