using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MacacosMazmorrasMVC.Controllers
{
    public class SessionController : Controller
    {
        private readonly SesionDAL sesionDAL;
        private readonly SheetCustomDAL sheetCustomDAL;
        private readonly MonsterDAL monsterDAL;
        private readonly SesionMonsterDAL sesionMonsterDAL;
        public SessionController()
        {
            sesionDAL = new SesionDAL(Conexion.StringBBDD);
            sheetCustomDAL = new SheetCustomDAL(Conexion.StringBBDD);
            monsterDAL = new MonsterDAL(Conexion.StringBBDD);
            sesionMonsterDAL = new SesionMonsterDAL(Conexion.StringBBDD);
        }
        public IActionResult Index()
        {
            int selectedSesionId = HttpContext.Session.GetInt32("_selectedSessionId") ?? 0;
            Sesion selectedSession = sesionDAL.ObtainSession(selectedSesionId);
            ViewBag.SelectedSession = selectedSession;

            SetPlayerList(GetFirstPlayerList());
            return View(GetPlayerList());
        }

        public IActionResult NewSesionForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewSesionForm(Sesion newSesion)
        {
            newSesion.FKCampaignId = HttpContext.Session.GetInt32("_selectedCampaignId") ?? 0;
            HttpContext.Session.SetInt32("_sessionId", newSesion.SesionId);
            //inject the info
            if (ModelState.IsValid)
            {
                sesionDAL.InsertSesion(newSesion);
                return RedirectToAction("CampaignSesions", "Campaign"); //redirect to Campaign
            }

            return View(newSesion);
        }

        public IActionResult UpdateSesionForm(int sessionId)
        {
            Sesion session = sesionDAL.ObtainSession(sessionId);
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSesionForm(Sesion session)
        {
            if (ModelState.IsValid)
            {
                sesionDAL.UpdateSesion(session);
                return RedirectToAction("CampaignSesions", "Campaign"); //redirect to Campaign with sessions
            }

            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSesion(int sesionId)
        {
            if (Request.Form["confirmed"] == "true")
            {
                sesionDAL.DeleteSesion(sesionId);

                return RedirectToAction("CampaignSesions", "Campaign");
            }

            return NoContent();
        }
        //
        //SESSION VARIABLES
        public List<SheetCustom> GetFirstPlayerList()
        {
            List<SheetCustom> sheetList = sheetCustomDAL.ObtainCampaignSheets(1);
            return sheetList;
        }
        public void SetPlayerList(List<SheetCustom> playerList)
        {
            // Serialize the list to JSON
            string serializedList = JsonConvert.SerializeObject(playerList);
            // Store the serialized string in the session
            HttpContext.Session.SetString("_PlayerList", serializedList);
        }
        List<SheetCustom> GetPlayerList()
        {
            string serializedList = HttpContext.Session.GetString("_PlayerList");
            List<SheetCustom> playerList = JsonConvert.DeserializeObject<List<SheetCustom>>(serializedList);

            return playerList;
        }
        public void SetSessionList(List<Unit> combatList)
        {
            // Serialize the list to JSON
            string serializedList = JsonConvert.SerializeObject(combatList);
            // Store the serialized string in the session
            HttpContext.Session.SetString("_CombatList", serializedList);
        }
        List<Unit> GetSessionList()
        {
            string serializedList = HttpContext.Session.GetString("_CombatList");
            List<Unit> combatList = JsonConvert.DeserializeObject<List<Unit>>(serializedList);

            return combatList;
        }
        // COMBAT METHODS
        public IActionResult StartCombat()
        {
            ViewBag.Position = 0;
            HttpContext.Session.SetInt32("_Position", 0);


            SetSessionList(GetUnorderedUnitList());
            ThrowInitiative();

            return View(GetSessionList());
        }
        public void ThrowInitiative()
        {
            int RandomNumber(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max + 1);
            }
            List<Unit> unorderedList = GetSessionList();

            List<Unit> orderedList = unorderedList
                .OrderByDescending(unit => (unit.Dex / 2) + RandomNumber(1, 20))
                .ThenBy(unit => (unit.Dex / 2) + RandomNumber(1, 20)) // Orders by another initiative throw in case of tie
                .ToList();

            SetSessionList(orderedList);
        }
        [HttpPost]
         public List<Unit> GetUnorderedUnitList()
        {
            int sessionId = HttpContext.Session.GetInt32("_sessionId") ?? 1;
            List<Unit> combatList = new List<Unit>();
            List<Monster> monsterList = monsterDAL.ObtainSesionMonsters(sessionId);
            List<SheetCustom> sheetList = GetPlayerList();
            combatList.AddRange(sheetList);
            combatList.AddRange(monsterList);
            
            foreach(SheetCustom player in combatList.OfType<SheetCustom>())
                player.IsPlayer = true;

            return combatList;
        }

        public IActionResult PassTurn()
        {
            List<Unit> combatList = GetSessionList();

            Unit firstUnit = combatList[0];
            combatList.RemoveAt(0);
            combatList.Add(firstUnit);
            combatList = CheckDeaths(combatList);

            SetSessionList(combatList);
            return View("StartCombat", combatList);
        }

        public List<Unit> CheckDeaths(List<Unit> combatList)
        {
            List<Unit> aliveUnits = new List<Unit>();

            foreach (Unit unit in combatList)
            {
                if (unit.SesionHp > 0 || unit.IsPlayer == true)
                    aliveUnits.Add(unit);
            }
            return aliveUnits;
        }
        public List<SheetCustom> GetPostCombatPlayers()
        {
            List<Unit> combatList = GetSessionList();
            List<Unit> playersToRemove = new List<Unit>();

            foreach (Unit unit in combatList)
            {
                if (unit.IsPlayer != true)
                    playersToRemove.Add(unit);
            }

            foreach (Unit playerToRemove in playersToRemove)
                combatList.Remove(playerToRemove);

            string serializedList = JsonConvert.SerializeObject(combatList);
            List<SheetCustom> playerList = JsonConvert.DeserializeObject<List<SheetCustom>>(serializedList);

            return playerList;
        }
        public IActionResult ChangeHp(int position, int newHp)
        {
            List<Unit> combatList = GetSessionList();
            combatList[position].SesionHp = newHp;
            SetSessionList(combatList);

            return View("StartCombat", combatList);
        }
        public IActionResult ChangePlayerHp(int position, int newHp)
        {
            List<SheetCustom> playerList = GetPlayerList();
            playerList[position].SesionHp = newHp;
            SetPlayerList(playerList);

            return View("Index", playerList);
        }
        public IActionResult EndCombat()
        {
            List<SheetCustom> playerList = GetPostCombatPlayers();
            return View("Index", playerList);
        }
        public IActionResult SetFocus(int position)
        {
                ViewBag.Position = position;
                return View("StartCombat", GetSessionList());
        }
    }
}
