using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace MonTPTest.Models
{
    public class StatistiqueString
    {
        private string m_nom;
        private string m_valeur;
        [Display(Name = "Nom de la statistique")]
        public string Nom
        {
            get
            {
                return m_nom;
            }
        }
        [Display(Name = "Valeur de la statistique")]
        public string Valeur
        {
            get
            {
                return m_valeur;
            }
            set
            {
                m_valeur = value;
            }
        }
        public StatistiqueString(string pData) : this(pData.Split(':')[0], pData.Split(':')[1])
        {
        }
        public StatistiqueString(string pNom, string pValeur)
        {
            m_nom = pNom;
            m_valeur = pValeur;
        }
        public override string ToString()
        {
            return m_nom + ":" + m_valeur.ToString();
        }
        public string ToStringForView()
        {
            return m_nom + ": " + m_valeur;
        }
        public bool Contains(string pValue)
        {
            return m_nom.ToUpper().Contains(pValue.ToUpper()) || m_valeur.ToUpper().Contains(pValue.ToUpper());
        }
        public static StatistiqueString Parse(string pData)
        {
            return new StatistiqueString(pData.Split(':')[0], pData.Split(':')[1]);
        }
        public static List<StatistiqueString> ParseList(string pData)
        {
            if(pData == "AUTO_RANDOM")
            {
                return RandomStats();
            }
            List<StatistiqueString> returnList = new List<StatistiqueString>();
            string[] stats = pData.Split(";");
            foreach (string stat in stats)
            {
                returnList.Add(StatistiqueString.Parse(stat));
            }
            return returnList;
        }
        public static bool Contains(List<StatistiqueString> pStats, string pValue)
        {
            bool ContainsValue = false;
            pStats.ForEach((StatistiqueString pStat) =>
            {
                if (pStat.Contains(pValue.ToUpper()))
                {
                    ContainsValue = true;
                }
            });
            return ContainsValue;
        }
        public static List<StatistiqueString> RandomStats()
        {
            Random random = new Random();
            List<StatistiqueString> returnList = new List<StatistiqueString>();
            returnList.Add(new StatistiqueString("VRAM", ((decimal)random.NextDouble() + (decimal)random.Next(0, 200)).ToString() + "GB"));
            returnList.Add(new StatistiqueString("PCI BANDWITH", ((decimal)random.NextDouble() + (decimal)random.Next(0, 800)).ToString() + "GB/S"));
            return returnList;
        }
    }
}
