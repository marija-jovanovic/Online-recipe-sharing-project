using Recepti.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Recepti.ViewModels
{
    public class ReceptCreateViewModel
    {
        [StringLength(200)]
        public string? Naslov { get; set; }

        [Display(Name = "Vreme na podgotovka")]
        [DataType(DataType.Time)]
        public DateTime? Vreme { get; set; }

        [Display(Name = "Slika")]
        public IFormFile? Slikaa { get; set; }

        public string? Tekst { get; set; }

        [Display(Name = "Povolno za vegani")]
        public string? Vegan { get; set; }

        [Display(Name = "Povolno za deca")]
        public string? Za_deca { get; set; }

        [Display(Name = "Tip na jadenje")]
        public string? Tip { get; set; }
        public string? Kreator { get; set; }

        public ICollection<Recenzija>? Recenzii { get; set; }


    }
}
