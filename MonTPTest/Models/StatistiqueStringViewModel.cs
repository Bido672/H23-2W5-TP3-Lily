using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
namespace MonTPTest.Models
{
    public class StatistiqueStringViewModel
    {
        [Display(Name ="Nom de la statistique")]
        [Required]
        public string Nom { get; set; }
        [Display(Name = "Valeur de la statistique")]
        [Required]
        public string Valeur { get; set; }
    }
}
