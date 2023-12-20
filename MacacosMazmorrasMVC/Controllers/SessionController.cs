using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.ViewModels;

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
            SetPlayerList(GetFirstPlayerList());

            var viewModel = new SessionViewModel()
            {
                SheetCustoms = GetPlayerList(),
                Monsters = GetSessionMonster(),
                Session = GetSession()
            };

            return View(viewModel);
        }
        #region SESSION MANAGEMENT
        //CRUD methods for session
        public Sesion GetSession()
        {
            int selectedSesionId = HttpContext.Session.GetInt32("_selectedSessionId") ?? 0;
            Sesion selectedSession = sesionDAL.ObtainSession(selectedSesionId);
            return selectedSession;
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
        #endregion
        #region UNIT VARIABLES MANAGEMENT
        //The following methods are used to set and get the unit list used both in the session and combat views.
        public List<Unit> GetSessionList()
        {
            string serializedList = HttpContext.Session.GetString("_CombatList");
            List<Unit> combatList = JsonConvert.DeserializeObject<List<Unit>>(serializedList);

            return combatList;
        }
        public List<Unit> GetUnorderedUnitList()
        {
            int sessionId = HttpContext.Session.GetInt32("_sessionId") ?? 1;

            List<Unit> combatList = new List<Unit>();
            string serializedList = HttpContext.Session.GetString("_EnemiesList");
            List<Monster> monsterList = JsonConvert.DeserializeObject<List<Monster>>(serializedList);

            List<SheetCustom> sheetList = GetPlayerList();
            combatList.AddRange(sheetList);
            combatList.AddRange(monsterList);

            foreach (SheetCustom player in combatList.OfType<SheetCustom>())
                player.IsPlayer = true;

            return combatList;
        }
        public List<SheetCustom> GetFirstPlayerList()
        {
            List<SheetCustom> sheetList = sheetCustomDAL.ObtainCampaignSheets(HttpContext.Session.GetInt32("_selectedCampaignId") ?? 1);
            return sheetList;
        }
        public List<SheetCustom> GetPlayerList()
        {
            string serializedList = HttpContext.Session.GetString("_PlayerList");
            List<SheetCustom> playerList = JsonConvert.DeserializeObject<List<SheetCustom>>(serializedList);

            return playerList;
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
        public List<Monster> GetSessionMonster()
        {
            int sessionId = HttpContext.Session.GetInt32("_sessionId") ?? 1;

            // Obtén los monstruos seleccionados según sus IDs
            var selectedMonsterList = new List<Monster>();
            List<Monster> monsterList = monsterDAL.ObtainSesionMonsters(sessionId);
            return monsterList;
        }
        public void SetPlayerList(List<SheetCustom> playerList)
        {
            // Serialize the list to JSON
            string serializedList = JsonConvert.SerializeObject(playerList);
            // Store the serialized string in the session
            HttpContext.Session.SetString("_PlayerList", serializedList);
        }
        public void SetSessionList(List<Unit> combatList)
        {
            // Serialize the list to JSON
            string serializedList = JsonConvert.SerializeObject(combatList);
            // Store the serialized string in the session
            HttpContext.Session.SetString("_CombatList", serializedList);
        }
        [HttpPost]
        public ActionResult SelectedMonsters(List<string> selectedMonsters)
        {
            var selectedMonsterList = new List<Monster>();
            List<Monster> monsterList = GetSessionMonster();

            foreach (var monster in monsterList)
            {
                if (selectedMonsters != null && selectedMonsters.Contains(monster.Name))
                {
                    selectedMonsterList.Add(monster);
                }
            }
            string serializedList = JsonConvert.SerializeObject(selectedMonsterList);
            // Store the serialized string in the session
            HttpContext.Session.SetString("_EnemiesList", serializedList);

            return RedirectToAction("Index");
        }
        #endregion
        #region COMBAT METHODS
        //Methods used in the combat view to navigate through all the combat steps
        public IActionResult StartCombat()
        {
            ViewBag.Position = 0;
            HttpContext.Session.SetInt32("_Position", 0);

            SetSessionList(GetUnorderedUnitList());
            ThrowInitiative();

            return View(GetSessionList());
        }
        public void ThrowInitiative()
        //Determines the order in which units go through their turns
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
        public IActionResult PassTurn()
        //Continues the combat with the following turn, putting the current first turn at the end of the list
        {
            List<Unit> combatList = GetSessionList();

            Unit firstUnit = combatList[0];
            combatList.RemoveAt(0);
            combatList.Add(firstUnit);
            combatList = CheckDeaths(combatList);

            SetSessionList(combatList);
            return View("StartCombat", combatList);
        }
        public IActionResult ChangeHp(int position, int newHp)
        //Changes the HP for all the units involved in the combat
        {
            List<Unit> combatList = GetSessionList();
            if (newHp == 999)
                combatList[position].SesionHp += 1;
            else if(newHp == 666)
                combatList[position].SesionHp -= 1;
            else
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
        public IActionResult ChooseEnemies()
        {
            int sessionId = HttpContext.Session.GetInt32("_sessionId") ?? 1;
            List<Monster> monsterList = monsterDAL.ObtainSesionMonsters(sessionId);

            return View("EnemiesModal", monsterList);
        }
        public IActionResult EndCombat()
        {
            var viewModel = new SessionViewModel()
            {
                SheetCustoms = GetPostCombatPlayers(),
                Monsters = GetSessionMonster(),
                Session = GetSession()
            };

            return View("Index", viewModel);
        }
        public IActionResult SetFocus(int position)
        {
                ViewBag.Position = position;
                return View("StartCombat", GetSessionList());
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
        #endregion
    }
}
