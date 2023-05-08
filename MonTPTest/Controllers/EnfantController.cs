using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Primitives;
using MonTPTest.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net.NetworkInformation;

namespace MonTPTest.Controllers
{
    public class EnfantController : Controller
    {
        private BaseDonnees m_baseDonnees;
        public EnfantController(BaseDonnees baseDonnees)
        {
            this.m_baseDonnees = baseDonnees;
        }
        

        public IActionResult Recherche(string ?id)
        {   
            string MotCle = "";
            int? Min = null;
            int? Max = null;
            string estVedette = "any";
            bool[] marque = { false, false, false };

                var queries = Request.Query.ToList();
                List<CarteGraphique> mesCartes = m_baseDonnees.Cartes.ToList();
                List<string> marques = new List<string> { };
                //var marquesQuery = queries.Find((query) => query.Key == "marque");
                //var queryMarque = queries.Find((query) => query.Key == "marque");
            queries.ForEach((query) =>
            {
                //Console.WriteLine("Query: " + query.Key.ToString() + " Value: " + query.Value.ToString());
                switch (query.Key)
                {
                    case "marque":
                        query.Value.ToList<string>().ForEach(value => {
                            
                            MarqueDeCarte? oMarque = m_baseDonnees.Marques.Find((MarqueDeCarte oMarque) => { return oMarque.Nom.ToUpper() == value.ToUpper(); });
                            if(marque != null)
                            {
                                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                                int idDeMarque = oMarque.Id -1;
                                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                                marque[idDeMarque] = true;
                                marques.Add(value.ToUpper());
                            }
                            
                        });
                        break;
                    case "marque1":
                        marque[0] = true;
                        marques.Add("NVIDIA");
                        break;
                    case "marque2":
                        marque[1] = true;
                        marques.Add("AMD");
                        break;
                    case "marque3":
                        marque[2] = true;
                        marques.Add("INTEL");
                        break;
                    case "motcle":
                        MotCle = query.Value;
                        break;
                    case "min":
                        Min = int.Parse(query.Value);
                        break;
                    case "max":
                        Max = int.Parse(query.Value);
                        break;
                    case "estvedette":
                        estVedette = query.Value;
                        break;

                }
            });
            if(marques.Count == 0)
            {
                m_baseDonnees.Marques.ForEach((MarqueDeCarte oMarque) => {
                    marques.Add(oMarque.Nom.ToUpper());
                    for(int i = 0; i < marque.Length; i++)
                    {
                        marque[i] = true;
                    }
                });
            }
            ViewDataRecherche data = new ViewDataRecherche(
                filter(m_baseDonnees.Cartes.ToList(), marques, estVedette, Min, Max, MotCle),
                m_baseDonnees.Marques, MotCle, Min, Max, estVedette, marque);
            return View("Recherche", data);
            

        }

        [HttpPost]
        public IActionResult Recherche(IFormCollection collection)
        {
            Func<IFormCollection, string, string> getKey = (IFormCollection col, string key) =>
            {
                return col.ToList().Find(item => { return item.Key == key; }).Value[0].ToString();
            };
            /*StringValues hell = new string[] { "fuck" };
            
            collection.TryGetValue("estVedette", out hell);*/
            collection.ToList().ForEach(item => Console.WriteLine("Item: " + item.Key + "  | Value: " + item.Value[0]));
            //Console.WriteLine(id == null ? "id was null" : "id was not null");
            string MotCle = getKey(collection, "MotCle");

            int? Min = null;
            try { 
                Min = int.Parse(getKey(collection, "Min"));
            }
            catch (Exception)
            {
                Min = null;
            }
            
            int? Max = null;
            try
            {
                Max = int.Parse(getKey(collection, "Max"));
            }
            catch (Exception)
            {
                Max = null;
            }
            string estVedette = getKey(collection, "estVedette");
            List<string> marques = new List<string> { };
            bool[] marque = { bool.Parse(getKey(collection, "marqueNvidia")), bool.Parse(getKey(collection, "marqueAMD")), bool.Parse(getKey(collection, "marqueIntel")) };
            if (marque[0])
            {
                marques.Add("NVIDIA");
            }
            if (marque[1])
            {
                marques.Add("AMD");
            }
            if (marque[2])
            {
                marques.Add("INTEL");
            }
            ViewDataRecherche data = new ViewDataRecherche(
                filter(m_baseDonnees.Cartes.ToList(), marques, estVedette, Min, Max, MotCle),
                m_baseDonnees.Marques, MotCle, Min, Max, estVedette, marque);
            return View("Recherche", data);


        }
        public List<CarteGraphique> filter(List<CarteGraphique> pCartesAFilter, List<string> pMarques, string pEstVedette, int? pMin, int? pMax, string pMotCle)
        {
            Func<CarteGraphique, bool> FiltreMarques = (CarteGraphique pCarte) =>
            {
                return pMarques.Contains(pCarte.Marque.Nom.ToUpper());
            };
            Func<CarteGraphique, bool> FiltreEstVedette = (CarteGraphique pCarte) =>
            {
                return pEstVedette == "yes" ? pCarte.EstVedette : (pEstVedette == "no" ? !pCarte.EstVedette : true);
                /* La ligne ci-dessus équivaut à:
                if (pEstVedette == "yes")
                {
                    return pCarte.EstVedette;
                }
                else if (pEstVedette == "no")
                {
                    return !pCarte.EstVedette;
                }
                else
                {
                    return true;
                }*/
            };
            Func<CarteGraphique, bool> FiltreMin = (CarteGraphique pCarte) =>
            {
                if(pMin == null)
                {
                    return true;
                }
                bool VRAM_CHECK = true;
                StatistiqueString? VRAM = pCarte.Statistiques.Find((StatistiqueString stat) => { return stat.Nom == "VRAM"; });
                if (VRAM != null)
                {
                    decimal Value = decimal.Parse(VRAM.Valeur.Replace("GB",""));
                    if((decimal)pMin > Value)
                    {
                        VRAM_CHECK = false;
                    }
                }
                else
                {
                    VRAM_CHECK = false;
                }
                bool BANDWITH_CHECK = true;
                return VRAM_CHECK && BANDWITH_CHECK;
            };
            Func<CarteGraphique, bool> FiltreMax = (CarteGraphique pCarte) =>
            {
                if (pMax == null)
                {
                    return true;
                }
                bool VRAM_CHECK = true;
                StatistiqueString? VRAM = pCarte.Statistiques.Find((StatistiqueString stat) => { return stat.Nom == "VRAM"; });
                if (VRAM != null)
                {
                    decimal Value = decimal.Parse(VRAM.Valeur.Replace("GB", ""));
                    if ((decimal)pMax < Value)
                    {
                        VRAM_CHECK = false;
                    }
                }
                else
                {
                    VRAM_CHECK = false;
                }
                bool BANDWITH_CHECK = true;
                return VRAM_CHECK && BANDWITH_CHECK;
            };
            Func<CarteGraphique, bool> FiltreMotCle = (CarteGraphique pCarte) =>
            {
                return pCarte.Description.ToUpper().Contains(pMotCle.ToUpper())
                    || pCarte.Nom.Contains(pMotCle.ToUpper()) 
                    || StatistiqueString.Contains(pCarte.Statistiques, pMotCle.ToUpper());
            };
            return pCartesAFilter.Where(FiltreMarques).ToList()
                .Where(FiltreEstVedette).ToList()
                .Where(FiltreMin).ToList()
                .Where(FiltreMax).ToList()
                .Where(FiltreMotCle).ToList();
        }


        public IActionResult Details(string id)
        {
            return View("Details", m_baseDonnees.TrouverCarte(id));
        }
    }
}
