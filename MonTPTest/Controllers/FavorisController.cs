using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonTPTest.Extensions;
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
            var enfantIDs = HttpContext.Session.Get<List<int>>("enfantsIDs");
            if (enfantIDs == null)
            {
                enfantIDs = new List<int>();
            }
            return View(m_baseDonnees.ObtenirFavoris(enfantIDs));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjouterUnFavoris(string id, IFormCollection collection)
        {
            Func<IFormCollection, string, string> getKey = (IFormCollection col, string key) =>
            {
                return col.ToList().Find(item => { return item.Key == key; }).Value[0].ToString();
            };
            var enfantIDs = HttpContext.Session.Get<List<int>>("enfantsIDs");
            if (enfantIDs == null)
            {
                enfantIDs = new List<int>();
            }
            try
            {
                enfantIDs.Add(int.Parse(getKey(collection, "id")));
                HttpContext.Session.Set<List<int>>("enfantsIDs", enfantIDs);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Enfant");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RetirerUnFavoris(string id, IFormCollection collection)
        {
            Func<IFormCollection, string, string> getKey = (IFormCollection col, string key) =>
            {
                return col.ToList().Find(item => { return item.Key == key; }).Value[0].ToString();
            };
            var enfantIDs = HttpContext.Session.Get<List<int>>("enfantsIDs");
            if (enfantIDs == null)
            {
                enfantIDs = new List<int>();
            }
            try
            {
                enfantIDs.Remove(int.Parse(getKey(collection, "id")));
                HttpContext.Session.Set<List<int>>("enfantsIDs", enfantIDs);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Enfant");
            }
            
        }
    }
}
