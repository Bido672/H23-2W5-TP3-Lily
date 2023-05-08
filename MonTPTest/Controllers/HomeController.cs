using Microsoft.AspNetCore.Mvc;
using MonTPTest.Models;

namespace MonTPTest.Controllers
{
    public class HomeController : Controller
    {
        private BaseDonnees m_baseDonnees;
        public HomeController(BaseDonnees baseDonnees)
        {
            this.m_baseDonnees = baseDonnees;
        }
        public IActionResult Index()
        {
            return View(m_baseDonnees.Marques);
        }
    }
}
