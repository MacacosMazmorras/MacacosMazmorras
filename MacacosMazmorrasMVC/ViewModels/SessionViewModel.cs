using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;
namespace MacacosMazmorrasMVC.ViewModels
{
    public class SessionViewModel
    {
        public List<SheetCustom> SheetCustoms {get;set;}
        public List<Monster> Monsters {get;set;}
        public Sesion Session { get; set; }
    }
}
