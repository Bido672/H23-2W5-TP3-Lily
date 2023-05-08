using Microsoft.AspNetCore.Mvc;
using MonTPTest.Models;

namespace MonTPTest.Controllers
{
    public class FavorisController : Controller
    {
        private BaseDonnees m_baseDonnees;
        public FavorisController(BaseDonnees baseDonnees)
        {
            this.m_baseDonnees = baseDonnees;
        }
        public IActionResult Index()
        {
            return View(m_baseDonnees.ObtenirFavoris(m_baseDonnees.Favoris));
        }
    }
}
