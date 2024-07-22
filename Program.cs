using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentApp.Data;
using StudentApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StudentApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for Entity Framework Core
builder.Services.AddDbContext<StudentAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAppContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container
builder.Services.AddScoped<PdfService>();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust session timeout as needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable session middleware
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");






// Endpoint for generating PDF invoice
app.MapPost("/api/invoice/generate", async context =>
{
    var pdfService = context.RequestServices.GetRequiredService<PdfService>(); // Resolve PdfService from DI

    // Deserialize request body to Invoice model
    var invoice = await context.Request.ReadFromJsonAsync<Invoice>();

    if (invoice == null)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid invoice data.");
        return;
    }

    var pdfBytes = pdfService.GenerateInvoicePdf(invoice);

    context.Response.ContentType = "application/pdf";
    await context.Response.Body.WriteAsync(pdfBytes);
});

app.Run();
