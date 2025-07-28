using BLL.AutoMapper;
using DAL;
using eStore.Components;
using Microsoft.EntityFrameworkCore;
using BLL.Services;
using DAL.Repositories;
using Blazored.LocalStorage;
using eStore.Components.Services;
using BLL.Interface;

namespace eStore {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Add Session support
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<SessionService>();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddDbContext<EStoreDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("eStoreDatabase")));
            builder.Services.AddScoped(typeof(EStoreDatabaseContext));

            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(BLLAutoMapper).Assembly));
            
            // Business Layer Services
            builder.Services.AddScoped<MemberService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<CartService>();
            
            // eStore Services
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();

            // Data Access Layer Repositories
            builder.Services.AddScoped<MemberRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<CartRepository>();

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductRepository>();

            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<CategoryRepository>();

            var app = builder.Build();

            // Create and Migrate DB
            using (var scope = app.Services.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<EStoreDatabaseContext>();
                db.Database.Migrate();

                // Ensure upload directories exist with better error handling
                try
                {
                    var fileStorage = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
                    var result = fileStorage.EnsureDirectoryExistsAsync().Result;
                    
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    if (result)
                    {
                        logger.LogInformation("Upload directories initialized successfully");
                    }
                    else
                    {
                        logger.LogWarning("Failed to initialize upload directories");
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error during upload directory initialization");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            // Enable session
            app.UseSession();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
