using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace MonTPTest.Models
{
    public class ViewDataRecherche
    {
        public List<CarteGraphique> Cartes { get; set; }
        public List<MarqueDeCarte> MarquesDeCartes { get; set; }

        public List<SelectListItem> estVedetteItems { get; set; }
        
        [Display(Name = "Recherche par mot-clé")]
        public string MotCle { get; set; }
        [Display(Name = "Recherche numérique")]
        public int? Min { get; set; }
        [Display(Name = "Recherche numérique")]
        public int? Max { get; set; }
        [Display(Name = "Carte Vedette:")]
        public string estVedette { get; set; }
        [Display(Name = "Marques de carte:")]
        public bool[] marques { get; set; }
        [Display(Name = "Nvidia")]
        public bool marqueNvidia { get { return marques[0]; }  }
        [Display(Name = "AMD")]
        public bool marqueAMD { get { return marques[1]; } }
        [Display(Name = "Intel")]
        public bool marqueIntel { get { return marques[2]; } }
        public ViewDataRecherche(List<CarteGraphique> pCartes, List<MarqueDeCarte> pMarqueDeCartes, string pMotCle, int? pMin, int? pMax, string pEstVedette, bool pMarque_nvidia, bool pMarque_amd, bool pMarque_intel) : this(pCartes, pMarqueDeCartes, pMotCle, pMin, pMax, pEstVedette, new bool[] { pMarque_nvidia, pMarque_amd, pMarque_intel })
        {
        }
        public ViewDataRecherche(List<CarteGraphique> pCartes, List<MarqueDeCarte> pMarqueDeCartes, string pMotCle, int? pMin, int? pMax, string pEstVedette, bool[] pMarques)
        {
            estVedetteItems = new List<SelectListItem>();
            estVedetteItems.Add(new SelectListItem { Value = "any", Text = "Peut-importe" });
            estVedetteItems.Add(new SelectListItem { Value = "yes", Text = "Oui" });
            estVedetteItems.Add(new SelectListItem { Value = "no", Text = "Non" });
            Cartes = pCartes;
            MarquesDeCartes = pMarqueDeCartes;
            MotCle = pMotCle;
            Min = pMin;
            Max = pMax;
            estVedette = pEstVedette;
            marques = pMarques;

        }
        public ViewDataRecherche(List<CarteGraphique> pCartes, List<MarqueDeCarte> pMarqueDeCartes) : this(pCartes, pMarqueDeCartes, "", null, null, "any", new bool[] { true, true, true })
        {
        }
    }
}
