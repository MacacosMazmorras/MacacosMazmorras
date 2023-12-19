using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.ViewModels;
using MacacosMazmorrasMVC.DAL;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAL usuarioDAL;
        private readonly SesionDAL sesionDAL;
        private readonly SheetCustomDAL sheetCustomDAL;
        private readonly CampaignDAL campaignDAL;

        public UsuarioController()
        {
            usuarioDAL = new UsuarioDAL(Conexion.StringBBDD);
            sesionDAL = new SesionDAL(Conexion.StringBBDD);
            sheetCustomDAL = new SheetCustomDAL(Conexion.StringBBDD);
            campaignDAL = new CampaignDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Sesion> lstSession = new List<Sesion>();
            List<SheetCustom> lstSheetCustom = sheetCustomDAL.ObtainUserSheets(usuarioId);
            List<Campaign> lstCampaigns = campaignDAL.ObtainAllUserCampaigns(usuarioId);

            foreach (Campaign campaign in lstCampaigns)
                lstSession.AddRange(sesionDAL.ObtainAllCampaignSesions(campaign.CampaignId));

            lstSession.OrderByDescending(s => s.SesionDate).ToList();

            Usuario userInfo = usuarioDAL.GetUserById(usuarioId);

            var viewModel = new UserHomeViewModel //Con esto podemos almacenar en un modelo toda la info
            {
                UserInfo = userInfo,
                Sessions = lstSession.TakeLast(2).ToList(),
                SheetCustoms = lstSheetCustom.TakeLast(3).ToList(),
                SessionCampaigns = lstCampaigns.TakeLast(2).ToList()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Usuario newUsuario)
        {
            if (ModelState.IsValid)
            {
                usuarioDAL.InsertUsuario(newUsuario);
                return RedirectToAction("Index", "Usuario"); //reditect to log in
            }

            return View(newUsuario);
        }

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User; //Get the Claims with its info

            if (claimUser.Identity.IsAuthenticated) //Verify if the user is authenticated and redirect to Home
                return RedirectToAction("Home", "Usuario");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("UsuarioMail,UsuarioPassword")] Usuario user)
        {

            Usuario sessionUser = usuarioDAL.GetUser(user);

            if (sessionUser != null)
            {
                #region User authorization (Log In)
                List<Claim> userSessions = new List<Claim>() //We create a Claims list which will save user information
                {
                    new Claim(ClaimTypes.NameIdentifier, sessionUser.UsuarioId.ToString()), //We add the user ID as the main session identifier
                    new Claim(ClaimTypes.Name, sessionUser.UsuarioName),
                };
                //This identity will contain the info of the userSession and the system we will use to authetify it
                ClaimsIdentity sessionIdentity = new ClaimsIdentity(userSessions, CookieAuthenticationDefaults.AuthenticationScheme); 
                //We use cookies to authentify users, in this case, based on the info we put in the claims list (userSessions)

                AuthenticationProperties properties = new AuthenticationProperties() //Here we specify the behaviour of the Authentication system
                {
                    AllowRefresh = true, //The session will remain between page refreshing
                    IsPersistent = true, //Will save the session in Cookies and remember for the time we execute the App
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, //We start session with HttpContext with the identity and the properties specified
                    new ClaimsPrincipal(sessionIdentity), properties);
                #endregion

                return RedirectToAction("Home", "Usuario"); //redirect to home
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(user);
            }
        }

        public IActionResult UserSettings()
        {
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Usuario userInfo = usuarioDAL.GetUserById(usuarioId);

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserSettings(Usuario userModified)
        {
            return RedirectToAction("Home", "Usuario"); //redirect to Home
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int userId)
        {
            if (Request.Form["confirmed"] == "true")
            {
                usuarioDAL.DeleteUser(userId);

                return RedirectToAction("Home", "Usuario");
            }

            return NoContent();
        }
    }
}