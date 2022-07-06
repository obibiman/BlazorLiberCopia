using Bibliographia.BlazorUI.Server.Providers;
using Bibliographia.BlazorUI.Server.ServiceClient.Base;
using Bibliographia.BlazorUI.Server.WebServicesProxy.Authentication;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//add blazored local storage
builder.Services.AddBlazoredLocalStorage();
//add the Client proxy
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7016"));
//add authentication service to pipeline
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//add this services first and then use it below
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
//add auth service provider and wrapper
builder.Services.AddScoped<ApiAuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthenticationStateProvider>());

//adding logging capabilities
builder.Logging.ClearProviders(); //clear all default providers
builder.Logging.AddConsole(); //log to console 
builder.Logging.AddDebug(); //log to debug window 

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
