using Bibliographia.BlazorUI.Server.Data;
using Bibliographia.BlazorUI.Server.Providers;
using Bibliographia.BlazorUI.Server.WebServicesProxy.Authentication;
using Bibliographia.BlazorUI.Server.WebServicesProxy.Base;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//add blazored local storage
builder.Services.AddBlazoredLocalStorage();
//add the Client proxy
builder.Services.AddHttpClient<IBibliographiaClient, BibliographiaClient>(cl => cl.BaseAddress = new Uri("https://localhost:7016"));

//add authentication service to pipeline
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//add this services first and then use it below
builder.Services.AddScoped<ApiAuthenticationStateProvider>();

//add auth service provider and wrapper
builder.Services.AddScoped<ApiAuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthenticationStateProvider>());

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
