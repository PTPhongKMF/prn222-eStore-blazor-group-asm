using BLL.AutoMapper;
using DAL;
using eStore.Components;
using Microsoft.EntityFrameworkCore;
using BLL.Services;
using DAL.Repositories;
using Blazored.LocalStorage;
using eStore.Components.Services;

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
            
            builder.Services.AddScoped<MemberService>();
            builder.Services.AddScoped<MemberRepository>();

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductRepository>();

            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<CategoryRepository>();

            var app = builder.Build();

            // Create and Migrate DB
            using (var scope = app.Services.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<EStoreDatabaseContext>();
                db.Database.Migrate(); 
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
