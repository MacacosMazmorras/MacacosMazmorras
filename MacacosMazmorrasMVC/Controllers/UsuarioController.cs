using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAL usuarioDAL;

        public UsuarioController()
        {
            usuarioDAL = new UsuarioDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            //var sessionId = HttpContext.Session.GetInt32("_UsuarioId"); //do a get from session
            //ViewBag.SessionId = sessionId; //pass de variable to a view
            return View();
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(Usuario newUsuario)
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
                    //new Claim("OtherProperties", "ExampleRole") //We can add more properties to the user session to get more info
                };

                ClaimsIdentity sessionIdentity = //This identity will contain the info of the userSession and the system we will use to authetify it
                new ClaimsIdentity(userSessions, CookieAuthenticationDefaults.AuthenticationScheme); //We use cookies to authentify users, in this case, based on the info we put in the claims list (userSessions)

                AuthenticationProperties properties = new AuthenticationProperties() //Here we specify the behaviour of the Authentication system
                {
                    AllowRefresh = true, //The session will remain between page refreshing
                    IsPersistent = true, //Will save the session in Cookies and remember for the time we execute the App
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, //We start session with HttpContext with the identity and the properties specified
                    new ClaimsPrincipal(sessionIdentity), properties);
                #endregion

                HttpContext.Session.SetInt32("_UsuarioId", sessionUser.UsuarioId); //create a session variable
                return RedirectToAction("Home", "Usuario"); //redirect to home
            }
            else
                return View();
        }
    }
}
