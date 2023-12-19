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
            int monstersQuantity = 10;
            List<Monster> lstMonsters = monsterDAL.ObtainAllMonstersPaged(page, monstersQuantity);

            int totalMonsters = monsterDAL.ObtainTotalMonsterCount();
            int totalPages = (int)Math.Ceiling((double)totalMonsters / monstersQuantity);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(lstMonsters);
        }

        //Spell
        public IActionResult Spell(int page = 1)
        {
            int spellsQuantity = 10; 
            List<Spell> lstSpells = spellDal.ObtainAllSpellsPaged(page, spellsQuantity);

            int totalSpells = spellDal.ObtainTotalSpellCount();
            int totalPages = (int)Math.Ceiling((double)totalSpells / spellsQuantity);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(lstSpells);
        }

        //search bar monster
        public IActionResult SearchMonster(string searchTerm, int page = 1)
        {
            int monsterQuantity = 10;
            int totalPages;

            if (searchTerm != null) 
            {
                List<Monster> lstSearchMonsterResults = monsterDAL.ObtainMonstersSearchResultPaged(searchTerm, page, monsterQuantity);

                int totalMonsters = monsterDAL.ObtainTotalMonsterCountSearched(searchTerm);

                if ( totalMonsters <= 0)
                {
                    List<Monster> lstMonsters = monsterDAL.ObtainAllMonstersPaged(page, monsterQuantity);

                    totalMonsters = monsterDAL.ObtainTotalMonsterCount();
                    totalPages = (int)Math.Ceiling((double)totalMonsters / monsterQuantity);

                    ViewData["CurrentPage"] = page;
                    ViewData["TotalPages"] = totalPages;

                    return View("Monster", lstMonsters);
                }

                totalPages = (int)Math.Ceiling((double)totalMonsters / monsterQuantity);
                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = totalPages;

                return View("Monster", lstSearchMonsterResults);
            }
            else
            {
                List<Monster> lstMonsters = monsterDAL.ObtainAllMonstersPaged(page, monsterQuantity);
                
                int totalMonsters = monsterDAL.ObtainTotalMonsterCount();
                totalPages = (int)Math.Ceiling((double)totalMonsters / monsterQuantity);

                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = totalPages;

                return View("Monster",lstMonsters);

            }

        }


        //search bar spells
        public IActionResult SearchSpells(string searchTerm, int page = 1)
        {
            int spellsQuantity = 10;
            int totalPages;

            if (searchTerm != null)
            {
                List<Spell> lstSearchSpellResults = spellDal.ObtainSpellsSearchResultPaged(searchTerm, page, spellsQuantity);

                int totalSpells = spellDal.ObtainTotalSpellsCountSearched(searchTerm);

                if (totalSpells <= 0)
                {
                    List<Spell> lstSpells = spellDal.ObtainAllSpellsPaged(page, spellsQuantity);

                    totalSpells = spellDal.ObtainTotalSpellCount();
                    totalPages = (int)Math.Ceiling((double)totalSpells / spellsQuantity);

                    ViewData["CurrentPage"] = page;
                    ViewData["TotalPages"] = totalPages;

                    return View("Spell",lstSpells);
                }

                totalPages = (int)Math.Ceiling((double)totalSpells / spellsQuantity);
                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = totalPages;

                return View("Spell", lstSearchSpellResults);
            }
            else
            {
                List<Spell> lstSpells = spellDal.ObtainAllSpellsPaged(page, spellsQuantity);

                int totalSpells = spellDal.ObtainTotalSpellCount();
                totalPages = (int)Math.Ceiling((double)totalSpells / spellsQuantity);

                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = totalPages;

                return View("Spell", lstSpells);

            }

        }
    }
}
