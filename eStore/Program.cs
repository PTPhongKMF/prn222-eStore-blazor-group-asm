using BLL.AutoMapper;
using DAL;
using eStore.Components;
using Microsoft.EntityFrameworkCore;

namespace eStore {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<EStoreDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("eStoreDatabase")));
            builder.Services.AddScoped(typeof(EStoreDatabaseContext));

            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(BLLAutoMapper).Assembly));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
