using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portfolio.Web;
using Portfolio.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// BaseAddress is what makes relative request paths work. HostEnvironment
// .BaseAddress is whatever <base href> resolved to, so "repos.json" becomes
// /Portfolio-WebRazor/repos.json in production and /repos.json locally,
// with no environment specific code. Without this, a relative URI throws.
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<GitHubService>();

await builder.Build().RunAsync();
