namespace MacacosMazmorrasMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            #region UserSession
            //Add caché memory use for the user session
            builder.Services.AddDistributedMemoryCache();
            //Add context accessor to get the session values
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSession(options =>
            {
                //SessionName
                options.Cookie.Name = "MacacosMazmorras.Session";
                //Time which the session will remain active in idle, if the time's passed, the session ends automatically
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                //It means is necessary to start a session to run the app
                options.Cookie.IsEssential = true;
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            #region Rutas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "LogIn",
                pattern: "LogIn",
                defaults: new { controller = "Usuario", action = "LogIn" });

            app.MapControllerRoute(
                name: "SignIn",
                pattern: "SignIn",
                defaults: new { controller = "Usuario", action = "SignIn" });

            app.MapControllerRoute(
                name: "Home",
                pattern: "Home",
                defaults: new { controller = "Home", action = "Home" });

            app.MapControllerRoute(
                name: "Campaign",
                pattern: "Campaign",
                defaults: new { controller = "Campaign", action = "Index" });

            app.MapControllerRoute(
                name: "NewCampaignForm",
                pattern: "NewCampaignForm",
                defaults: new { controller = "Campaign", action = "NewCampaignForm" });

            app.MapControllerRoute(
                name: "UpdateCampaignForm",
                pattern: "UpdateCampaignForm",
                defaults: new { controller = "Campaign", action = "UpdateCampaignForm" });

            app.MapControllerRoute(
                name: "Spells",
                pattern: "Spells",
                defaults: new { controller = "Glossary", action = "Spell" });

            app.MapControllerRoute(
                name: "Spells",
                pattern: "Spells",
                defaults: new { controller = "Glossary", action = "Monster" });
            #endregion

            app.Run();
        }
    }
}