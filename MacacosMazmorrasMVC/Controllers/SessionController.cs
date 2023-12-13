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
        public SessionController()
        {
            sesionDAL = new SesionDAL(Conexion.StringBBDD);
            sheetCustomDAL = new SheetCustomDAL(Conexion.StringBBDD);
            monsterDAL = new MonsterDAL(Conexion.StringBBDD);
        }
        public IActionResult Index()
        {
            return View();
        }
        //SESSION VARIABLES
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
            SetSessionList(GetUnorderedUnitList());
            ThrowInitiative();
            List<Unit> combatList = GetSessionList();
            return View(combatList);
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
            List<Unit> combatList = new List<Unit>();
            List<Monster> monsterList = monsterDAL.ObtainSesionMonsters(1);
            List<SheetCustom> sheetList = sheetCustomDAL.ObtainUserSheets(1);
            combatList.AddRange(sheetList);
            combatList.AddRange(monsterList);
            return combatList;
        }

        public void PassTurn()
        {
            List<Unit> combatList = GetSessionList();

            Unit firstUnit = combatList[0];
            combatList.RemoveAt(0);
            combatList.Add(firstUnit);
            combatList = CheckDeaths(combatList);

            SetSessionList(combatList);
        }

        public List<Unit> CheckDeaths(List<Unit> combatList)
        {
            foreach (Monster monster in combatList)
            {
                if (monster.Hp <= 0)
                    combatList.Remove(monster);
            }
            return combatList;
        }
    }
}
