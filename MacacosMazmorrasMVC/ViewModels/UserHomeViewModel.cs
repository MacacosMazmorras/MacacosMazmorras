using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.ViewModels
{
    public class UserHomeViewModel
    {
        public Usuario UserInfo { get; set; }
        public List<Sesion> Sessions { get; set; }
        public List<SheetCustom> SheetCustoms { get; set; }
        public List<Campaign> SessionCampaigns { get; set; }

    }
}
