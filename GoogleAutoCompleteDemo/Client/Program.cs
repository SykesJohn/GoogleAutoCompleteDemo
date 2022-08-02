using GoogleAutoCompleteDemo.Client;
using GoogleAutoCompleteDemo.Client.Pages;
using GoogleAutoCompleteDemo.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var dir = Directory.GetCurrentDirectory();

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add Telerik Blazor server side services
builder.Services.AddTelerikBlazor();

var app = builder.Build();
AutoComplete.GoogleApiKey=app.Configuration["GoogleApiKey"];
await app.RunAsync();
