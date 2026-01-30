using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProductsProjectCRUD.Models;
using ProductsProjectCRUD.BusinessLogicService;
using System.Text.Json;
// Make sure this namespace matches where your Product, Category, Supplier models are defined
// Assuming ProductService is in the root of your project or has a defined namespace
// If ProductService is in a folder like 'Services', you'd use 'YourApp.Services'
// For this example, let's assume it's directly in the project root or 'YourApp.Data' if you moved it.
// I'll keep it simple for now, assuming it's accessible.
// If you put ProductService in a 'Services' folder, you might need:
// using YourApp.Services;

namespace ProductsProjectCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            
            var builder = WebApplication.CreateBuilder(args);
            
            
            // Add services to the container.
            builder.Services.AddRazorPages()
                .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            // Configure the ProductService
            // Get the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("ConnectionStringProducts");





            // Register ProductService with the Dependency Injection container
            // We're registering it as a Scoped service, which means a new instance will be created
            // for each incoming HTTP request. This is generally suitable for data access services.
            // Configure the ProductService (DAL)
            builder.Services.AddScoped<ProductService>(sp =>
            {
                // When ProductService needs an IConfiguration, the DI container will provide it.
                // This ensures ProductService gets the connection string from appsettings.json.
                // Pass the IConfiguration to ProductService
                return new ProductService(sp.GetRequiredService<IConfiguration>());
            });


            // Register the ProductDataAccess (Service Layer)
            // ProductDataAccess depends on ProductService, which the DI container can now provide.
            builder.Services.AddScoped<ProductDataAccess>();









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
        }
    }
}
