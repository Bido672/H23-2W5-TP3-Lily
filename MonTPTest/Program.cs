using Microsoft.AspNetCore.Builder;
using MonTPTest.Models;
using System;

var builder = WebApplication.CreateBuilder(args); // Crée une web app avec les paramètres envoyés
builder.Services.AddControllersWithViews(); // Permet MVC
builder.Services.AddRazorPages(); // Permet utilisation de Razor
builder.Services.AddMvc().AddRazorRuntimeCompilation();
//builder.
builder.Services.AddSingleton<BaseDonnees>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(20); });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = context => context.Context.Response.Headers.Add("Cache-Control", "no-cache")
    });
}
else
{
    app.UseStaticFiles();
}
app.UseSession();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "gestionEnfantDelete",
        pattern: "/GestionEnfant/delete/{id}",
        defaults: new { controller = "Editor", action = "Delete" });
    endpoints.MapControllerRoute(
        name: "favoris",
        pattern: "/favoris",
        defaults: new { controller = "Favoris", action = "Index" });
    endpoints.MapControllerRoute(
        name: "recherche",
        pattern: "recherche/{id?}",
        defaults: new { controller = "Enfant", action = "Recherche" });
    endpoints.MapControllerRoute(
        name: "enfantPattern1",
        pattern: "carte/details/{id?}",
        defaults: new { controller = "Enfant", action = "Details" });
    endpoints.MapControllerRoute(
        name: "enfantPattern2",
        pattern: "carte/{id?}",
        defaults: new { controller = "Enfant", action = "Details" });
    endpoints.MapControllerRoute(
        name: "enfantPattern3",
        pattern: "/{id}",
        defaults: new { controller = "Enfant", action = "Details" });
    endpoints.MapControllerRoute(
        name: "enfantPattern3",
        pattern: "/",
        defaults: new { controller = "Home", action = "Index" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "Home", action = "Index" });
});

app.MapRazorPages();
app.Run();

// Doc
// Environnements: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0
// Fichiers statiques et wwwroot : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0
// Gestion de la cache : https://learn.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-7.0