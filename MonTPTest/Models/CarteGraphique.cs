using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace MonTPTest.Models
{
    public class CarteGraphique
    {
        private int m_id;
        public int Id { get { return m_id; } private set {
                m_id = value;
            } }
        public static void SetID(List<CarteGraphique> pCartes, CarteGraphique pCarte, int pNewID)
        {
            if(pCartes.Where((CarteGraphique fCarte) => { return fCarte.Id == pNewID; }).ToList().Count != 0)
            {
                throw new ArgumentException("ID Déja utilisé");
            }
            else
            {
                pCarte.m_id = pNewID;
            }
        }
        public int IdParent { get { return m_marque.Id;  } }
        private MarqueDeCarte m_marque;
        private string m_nom;
        public string Nom
        {
            get
            {
                return m_nom;
            }
        }
        private string m_description;
        public string Description
        {
            get
            {
                return m_description;
            }
        }
        public MarqueDeCarte Marque { 
            get 
            { 
                return m_marque; 
            } 
        }
        public string ImageCarte
        {
            get
            {
                //Console.WriteLine(FileSystem.CurDir() + "\\wwwroot\\images\\cartes\\" + m_nom.ToUpper() + ".png");
                string ImageDeLaCarte = FileSystem.CurDir() + "\\wwwroot\\images\\cartes\\" + m_nom.ToUpper() + ".png";
                if (File.Exists(ImageDeLaCarte))
                {
                    ImageDeLaCarte = "/images/cartes/" + m_nom.ToUpper() + ".png";
                }
                else
                {
                    ImageDeLaCarte = "/images/cartes/default/" + m_marque.Nom.ToLower() + ".png";
                }
                return ImageDeLaCarte;
            }
        }
        private bool m_vedette;
        public bool EstVedette
        {
            get
            {
                return m_vedette;
            }
        }
        private List<StatistiqueString> m_statistiques;
        public List<StatistiqueString> Statistiques
        {
            get
            {
                return m_statistiques;
            }
        }
        public CarteGraphique(int pId, string pNomCarte, string pDescription, List<StatistiqueString> pStatistiques, bool pEstVedette, MarqueDeCarte pMarque)
        {
            
            if(pMarque == null)
            {
                throw new ArgumentNullException();
            }
            if (pStatistiques == null)
            {
                throw new ArgumentNullException();
            }
            m_id = pId;
            m_statistiques = pStatistiques;
            m_nom = pNomCarte;
            m_marque = pMarque;
            m_description = pDescription;
            m_vedette = pEstVedette;
        }
        public CarteGraphique(string pData, MarqueDeCarte pMarque) : this(int.Parse(pData.Split('|')[0]), pData.Split('|')[1], pData.Split('|')[2], StatistiqueString.ParseList(pData.Split('|')[3]), bool.Parse(pData.Split('|')[4]),pMarque)
        {
            if (pMarque == null)
            {
                throw new ArgumentNullException();
            }
        }
        public override string ToString()
        {
            return string.Join("|", new string[] { Id.ToString(), Nom, Description, string.Join(";", Statistiques), EstVedette ? "true" : "false", Marque.Nom.ToUpper(), Marque.Id.ToString() }) ;
        }
        public string ToStringForView()
        {
            return m_marque.Nom + " " + m_nom;
        }
        public static CarteGraphique Parse(string pData, MarqueDeCarte pMarque)
        {
            if (pMarque == null)
            {
                throw new ArgumentNullException();
            }
            string[] dataSplit = pData.Split('|');
            /*
                Exemple de string
                ID|NOM_CARTE|DESCRIPTION_CARTE|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|EstVedette
             */
            int pId = int.Parse(dataSplit[0]);
            string pNomCarte = dataSplit[1];
            string pDescription = dataSplit[2];
            List<StatistiqueString> pStatistiques = StatistiqueString.ParseList(dataSplit[3]);
            bool pEstVedette = bool.Parse(dataSplit[4]);
            return new CarteGraphique(pId, pNomCarte,pDescription,pStatistiques,pEstVedette,pMarque);
        }
        public static CarteGraphique Parse(string pData, List<MarqueDeCarte> pMarques)
        {
            if (pMarques == null)
            {
                throw new ArgumentNullException();
            }
            string[] dataSplit = pData.Split('|');
            /*
                Exemple de string
                ID|NOM_CARTE|DESCRIPTION_CARTE|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|EstVedette|MARQUE|IDMARQUE
             */
            int pId = int.Parse(dataSplit[0]);
            string pNomCarte = dataSplit[1];
            string pDescription = dataSplit[2];
            List<StatistiqueString> pStatistiques = StatistiqueString.ParseList(dataSplit[3]);
            bool pEstVedette = bool.Parse(dataSplit[4]);
            MarqueDeCarte? pMarque = null;
            try
            {
                pMarque = pMarques.Find((MarqueDeCarte marqueATrouver) => { return marqueATrouver.Nom.ToUpper() == dataSplit[5]; });
            }
            catch (Exception e)
            {
                try
                {
                    pMarque = pMarques.Find((MarqueDeCarte marqueATrouver) => { return marqueATrouver.Id == int.Parse(dataSplit[6]); });
                }
                catch (Exception ee)
                {
                    throw new ArgumentException("Marque Invalide");
                }
            }
            return new CarteGraphique(pId, pNomCarte, pDescription, pStatistiques, pEstVedette, pMarque);
        }
        public static CarteGraphique Random(BaseDonnees pBD) 
        {
            return Random(pBD.Cartes, pBD.Marques);
        }
        public static CarteGraphique Random(List<CarteGraphique> pCartes, List<MarqueDeCarte> pMarques)
        {
            return Random(pCartes.Count, pCartes, pMarques);
        }
        public static CarteGraphique Random(int pId, List<CarteGraphique> pCartes, List<MarqueDeCarte> pMarques)
        {
            Random random = new Random();
            MarqueDeCarte pMarque = pMarques[random.Next(0, pMarques.Count - 1)];
            string pNomCarte = "";
            bool done = false;
            do
            {
                switch (pMarque.Nom.ToUpper())
                {
                    case "NVIDIA":
                        int type = random.Next(0, 3);
                        pNomCarte += type == 0 ? "RTX" : "";
                        pNomCarte += type == 1 ? "GTX" : "";
                        pNomCarte += type == 2 ? "GT" : "";
                        pNomCarte += type == 3 ? "GS" : "";
                        int gen = random.Next(100, 4090);
                        pNomCarte += (gen).ToString();
                        pNomCarte += random.Next(0, 1) == 0 ? "TI" : "";
                        break;
                    case "AMD":
                        pNomCarte += "RX";
                        pNomCarte += random.Next(100, 5000).ToString();
                        break;
                    case "Intel":
                        pNomCarte += "A";
                        pNomCarte += random.Next(750, 800).ToString();
                        break;
                }
                done = pCartes.Where((CarteGraphique carteAVérifier) => {
                   return carteAVérifier.Nom == pNomCarte;
                }).ToList().Count == 0;
            } while (!done);
            return new CarteGraphique(pId, pNomCarte, "Carte généré aléatoirement", StatistiqueString.RandomStats(), random.Next(0, 1) == 0 ? true : false, pMarque);
        }
    }
}
