using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MonTPTest.Models
{
    public class MarqueDeCarte
    {
        private int m_id;
        public int Id { get { return m_id; } private set { m_id = value; } }
        private string m_nom;
        private List<CarteGraphique> m_listeCartes;
        public List<CarteGraphique> Cartes
        {
            get
            {
                return m_listeCartes;
            }
        }
        public string Nom
        {
            get
            {
                return m_nom;
            }
        }
        public string LienImageLogo
        {
            get
            {
                return "./images/marques/" + m_nom.ToLower() + ".png";
            }
        }
        private string m_description;
        public string Description
        {
            get { return m_description; }
        }
        private List<StatistiqueString> m_statistiques;
        public List<StatistiqueString> Statistiques
        {
            get
            {
                return m_statistiques;
            }
        }
       
        public MarqueDeCarte(string pData) : this(int.Parse(pData.Split('|')[0]), pData.Split('|')[1], pData.Split('|')[2], StatistiqueString.ParseList(pData.Split('|')[3]))
        {
            /*
                Exemple de string
                ID|NOM_MARQUE|DESCRIPTIONMARQUE|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE
             */

        }
        public MarqueDeCarte(int id, string pNomMarque, string pDescription, List<StatistiqueString> pStatistiques)
        {
            
            if (pStatistiques == null)
            {
                throw new ArgumentNullException();
            }
            m_id = id;
            m_nom = pNomMarque;
            m_listeCartes = new List<CarteGraphique> { };
            m_description = pDescription;
            m_statistiques = pStatistiques;
        }
        
        public override string ToString()
        {
            return string.Join("|", new string[] { Id.ToString(), Nom, Description, string.Join(";", m_statistiques) });
        }
        public string ToStringForView()
        {
            return Nom;
        }
        public static MarqueDeCarte Parse(string pData)
        {
            if (String.IsNullOrWhiteSpace(pData)) throw new ArgumentException(pData);
            string[] dataSplit = pData.Split('|');

            int id = int.Parse(dataSplit[0]);
            string nom = dataSplit[1];
            string description = dataSplit[2];
            List<StatistiqueString> statistiques = StatistiqueString.ParseList(dataSplit[3]);

            return new MarqueDeCarte(id, nom, description, statistiques);
        }
        public static void AddAllChildren(MarqueDeCarte pMarque, List<CarteGraphique> pTousLesCartes)
        {
            pMarque.Cartes.Clear();
            foreach (CarteGraphique pCarte in pTousLesCartes)
            {
                if(pCarte.Marque.Id == pMarque.Id)
                {
                    pMarque.m_listeCartes.Add(pCarte);
                }
            }
        }
    }
}
