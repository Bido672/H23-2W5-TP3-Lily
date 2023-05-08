namespace MonTPTest.Models
{
    public class DetailsViewModel
    {
        public CarteGraphique? Carte { get; set; }
        public bool? EstFavoris { get; set; } // Extra :3
        public DetailsViewModel(CarteGraphique? carte, bool? estFavoris)
        {
            Carte = carte;
            EstFavoris = estFavoris;
        }
    }
}
