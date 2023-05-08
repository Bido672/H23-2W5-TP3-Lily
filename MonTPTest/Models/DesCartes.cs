namespace MonTPTest.Models
{
    public class DesCartes
    {
        public List<string> Favoris
        {
            get
            {
                return new List<string> { "GTX1060", "RTX3080", "A750" };
            }
        }
        private List<MarqueDeCarte> m_marques;
        public List<MarqueDeCarte> Marques
        {
            get
            {
                return m_marques;
            }
        }
        private List<CarteGraphique> m_cartes;
        public List<CarteGraphique> Cartes
        {
            get
            {
                return m_cartes;
            }
        }
        public DesCartes()
        {
            m_marques = new List<MarqueDeCarte> { 
                new MarqueDeCarte("1|Nvidia|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE"),
                new MarqueDeCarte("2|AMD|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE"),
                new MarqueDeCarte("3|Intel|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE")
            };
            m_cartes = new List<CarteGraphique>
            {
                new CarteGraphique("1|GTX4090|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("2|GTX960|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("3|GTX860|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("4|GTX1060|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("5|GTX1070|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("6|GTX1080|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("7|RTX2060|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("8|RTX2070|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("9|RTX2080|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("10|RTX3050|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("11|RTX3060|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("12|RTX3070|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("13|RTX3080|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("14|RTX3090|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[0]),
                new CarteGraphique("15|RX570|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[1]),
                new CarteGraphique("16|RX580|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[1]),
                new CarteGraphique("17|RX590|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[1]),
                new CarteGraphique("18|A750|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[2]),
                new CarteGraphique("19|A770|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false", m_marques[2]),
            };
        }
        public List<CarteGraphique> ObtenirFavoris(List<string> pFavoris)
        {
            return m_cartes.Where((CarteGraphique maCarte) => { return pFavoris.Contains(maCarte.Nom); }).ToList();
        }
    }
}
