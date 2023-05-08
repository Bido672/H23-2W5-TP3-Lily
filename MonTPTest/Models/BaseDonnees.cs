using Microsoft.VisualBasic;
using System.Drawing.Imaging;

namespace MonTPTest.Models
{
    public class BaseDonnees
    {
        private const string BD_PATH_CARTES = "\\bd\\cartes.txt";
        private const string BD_PATH_MARQUES = "\\bd\\marques.txt";
        private List<CarteGraphique> m_cartes;
        private List<MarqueDeCarte> m_marques;
        public List<CarteGraphique> Cartes
        {
            get
            {
                return m_cartes;
            }
        }
        public List<MarqueDeCarte> Marques
        {
            get
            {
                return m_marques;
            }
        }
        public List<string> Favoris
        {
            get
            {
                return new List<string> { "GTX1060", "RTX3080", "A750" };
            }
        }
        public void Remove(CarteGraphique pCarte) {
            int removedID = pCarte.Id;
            m_cartes.Remove(pCarte);
            RenumberCards(removedID);
        }
        public void Add(CarteGraphique pCarte)
        {
            m_cartes.Add(pCarte);
        }
        public void Add(string pCarte, MarqueDeCarte pMarque)
        {
            m_cartes.Add(CarteGraphique.Parse(pCarte, pMarque));
        }
        public void Add()
        {
            Add(CarteGraphique.Random(this));
        }
        private void RenumberCards(int removedCardID)
        {
            for(int i = removedCardID-1; i < m_cartes.Count; i++)
            {
                CarteGraphique.SetID(m_cartes, m_cartes[i], i + 1);
            }
        }

        public BaseDonnees()
        {
            ChargerBD();
            /*m_marques = new List<MarqueDeCarte> {
                MarqueDeCarte.Parse("1|Nvidia|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE"),
                MarqueDeCarte.Parse("2|AMD|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE"),
                MarqueDeCarte.Parse("3|Intel|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE")
            };
            m_cartes = new List<CarteGraphique>
            {
                CarteGraphique.Parse("1|GTX4090|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|true|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("2|GTX960|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("3|GTX860|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("4|GTX1060|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("5|GTX1070|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("6|GTX1080|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("7|RTX2060|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("8|RTX2070|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("9|RTX2080|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("10|RTX3050|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("11|RTX3060|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("12|RTX3070|Funny|VRAM:15.50GB;PCI BANDWITH:15.60GB/S|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("13|RTX3080|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("14|RTX3090|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|NVIDIA|1", m_marques[0]),
                CarteGraphique.Parse("15|RX570|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|AMD|2", m_marques[1]),
                CarteGraphique.Parse("16|RX580|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|AMD|2", m_marques[1]),
                CarteGraphique.Parse("17|RX590|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|AMD|2", m_marques[1]),
                CarteGraphique.Parse("18|A750|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|INTEL|3", m_marques[2]),
                CarteGraphique.Parse("19|A770|Funny|NOM_STATISTIQUE:STATISTIQUE;NOM_STATISTIQUE:STATISTIQUE|false|INTEL|3", m_marques[2]),
            };*/
            m_marques.ForEach((MarqueDeCarte oMarque) =>
            {
                MarqueDeCarte.AddAllChildren(oMarque, m_cartes);
            });
            SavegarderBD();
        }
        public List<CarteGraphique> ObtenirFavoris(List<string> pFavoris)
        {
            return m_cartes.Where((CarteGraphique maCarte) => { return pFavoris.ToArray().Contains(maCarte.Nom); }).ToList();
        }
        public CarteGraphique? TrouverCarte(string id)
        {
            CarteGraphique? carteAVisioner = null;
            try
            {
                carteAVisioner = m_cartes.Find((CarteGraphique carteATrouver) => { return carteATrouver.Id == int.Parse(id.ToUpper()); });
            }
            catch (Exception)
            {
                carteAVisioner = m_cartes.Find((CarteGraphique carteATrouver) => { return carteATrouver.Nom == id.ToUpper(); });
            }
            return carteAVisioner;
        }
        public void ChargerBD()
        {
            string BDCartesPath = FileSystem.CurDir() + BD_PATH_CARTES;
            string BDMarquesPath = FileSystem.CurDir() + BD_PATH_MARQUES;
            // Charger les Marques de la mini bd en fichier texte
            m_marques = new List<MarqueDeCarte>();
            StreamReader marquesReader = new StreamReader(BDMarquesPath);
            do
            {
                try
                {
                    m_marques.Add(MarqueDeCarte.Parse(marquesReader.ReadLine()));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (!marquesReader.EndOfStream);
            marquesReader.Close();
            // Charger les cartes de la mini bd en fichier texte
            this.m_cartes = new List<CarteGraphique>();
            StreamReader cartesReader = new StreamReader(BDCartesPath);
            do
            {
                try
                {
                    m_cartes.Add(CarteGraphique.Parse(cartesReader.ReadLine(), m_marques));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (!cartesReader.EndOfStream);
            cartesReader.Close();
        }
        public void SavegarderBD()
        {
            // sauvegarder les cartes dans la mini bd
            string BDCartesPath = FileSystem.CurDir() + BD_PATH_CARTES;
            StreamWriter CartesWriter = new StreamWriter(BDCartesPath);
            this.Cartes.ForEach((CarteGraphique carteAEcrire) =>
            {
                CartesWriter.WriteLine(carteAEcrire.ToString());
            });
            CartesWriter.Close();
            // sauvegarder les marques dans la mini bd
            string BDMarquesPath = FileSystem.CurDir() + BD_PATH_MARQUES;
            StreamWriter MarquesWriter = new StreamWriter(BDMarquesPath);
            this.Marques.ForEach((MarqueDeCarte MarqueAEcrire) =>
            {
                MarquesWriter.WriteLine(MarqueAEcrire.ToString());
            });
            MarquesWriter.Close();
        }
    }
}
