using MacacosMazmorrasMVC.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<ImageBBDAL>(); //Esto registra ImageBBDAL como un servicio de ámbito, lo que significa que se creará una nueva instancia del servicio para cada solicitud.

            #region UserAuthentication
            //We specify to the project that we will use the Authentication system to protect some elements
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Usuario/LogIn"; //This is the page where anonymous users will be redirect if they are not authorized
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(120); //This is the time that the cookie will last, then the user will have to log in again
                });
            #endregion

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            #region Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Home}/{id?}");

            //login and register pages
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
            
            //campaign page
            app.MapControllerRoute(
                name: "Campaign",
                pattern: "Campaign",
                defaults: new { controller = "Campaign", action = "Index" });
            
            //we need put the campaign-session page (we show all the sesions in 1 campaign);
            app.MapControllerRoute(
                name: "NewCampaignForm",
                pattern: "NewCampaignForm",
                defaults: new { controller = "Campaign", action = "NewCampaignForm" });

            app.MapControllerRoute(
                name: "UpdateCampaignForm",
                pattern: "UpdateCampaignForm",
                defaults: new { controller = "Campaign", action = "UpdateCampaignForm" });
            //session page
            app.MapControllerRoute(
                name: "Session",
                pattern: "Session",
                defaults: new { controller = "Session", action = "Index" });
            //sheet custom page
            app.MapControllerRoute(
                name: "SheetCustom",
                pattern: "SheetCustom",
                defaults: new { controller = "SheetCustom", action = "SheetCustom" });

            app.MapControllerRoute(
                name: "NewSheetCustomForm",
                pattern: "NewSheetCustomForm",
                defaults: new { controller = "SheetCustom", action = "NewSheetCustomForm" });
            app.MapControllerRoute(
                name: "UpdateSheetCustomForm",
                pattern: "UpdateSheetCustomForm",
                defaults: new { controller = "SheetCustom", action = "UpdateSheetCustomForm" });

            //glossary page
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

            //image
            app.MapControllerRoute(
                name: "ImageBB",
                pattern: "ImageBB",
                defaults: new { controller = "ImageBB", action = "Index" });
        }

 
    }
}