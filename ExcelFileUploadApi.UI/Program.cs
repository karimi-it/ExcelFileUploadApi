using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

// تنظیم حداکثر اندازه مجاز برای بدن درخواست در Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
