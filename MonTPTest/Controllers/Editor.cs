using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonTPTest.Models;

namespace MonTPTest.Controllers
{
    public class Editor : Controller
    {
        private BaseDonnees m_baseDonnees;
        public Editor(BaseDonnees baseDonnees)
        {
            this.m_baseDonnees = baseDonnees;
        }
        // GET: Editor
        //public ActionResult Index()
        //{
        //    return View(m_baseDonnees.Cartes);
        //}

        // GET: Editor/Details/5
        //public ActionResult Details(string id)
        //{
        //    return View(m_baseDonnees.TrouverCarte(id));
        //}

        // GET: Editor/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Editor/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        /*{
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
        /*
        // GET: Editor/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Editor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
        // GET: Editor/Delete/5
        public ActionResult Delete(string id)
        {
            return View(m_baseDonnees.TrouverCarte(id));
        }

        // POST: Editor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
