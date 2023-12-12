using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class GlossaryController : Controller
    {
        private readonly MonsterDAL monsterDAL;
        private readonly SpellDAL spellDal;
        public GlossaryController()
        {
            monsterDAL = new MonsterDAL(Conexion.StringBBDD);
            spellDal = new SpellDAL(Conexion.StringBBDD);
        }

        //Monster
        public IActionResult Monster(int page = 1)
        {
            int monstersQuanity = 10;
            List<Monster> lstMonsters = monsterDAL.ObtainAllMonstersPaged(page, monstersQuanity);

            int totalMonsters = monsterDAL.ObtainTotalMonsterCount();
            int totalPages = (int)Math.Ceiling((double)totalMonsters / monstersQuanity);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(lstMonsters);
        }

        //Spell
        public IActionResult Spell(int page = 1)
        {
            int spellsQuanity = 10; 
            List<Spell> lstSpells = spellDal.ObtainAllSpellsPaged(page, spellsQuanity);

            int totalSpells = spellDal.ObtainTotalSpellCount();
            int totalPages = (int)Math.Ceiling((double)totalSpells / spellsQuanity);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(lstSpells);
        }
    }
}
