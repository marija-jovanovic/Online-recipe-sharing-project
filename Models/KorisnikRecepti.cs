using System.ComponentModel.DataAnnotations;

namespace Recepti.Models
{
    public class KorisnikRecepti
    {
        public int Id { get; set; }

        [Display(Name = "Korisnicko ime")]
        public string? korisnickoIme { get; set; }

        public int ReceptiId { get; set; }

        public Recept? Recept { get; set; }
    }
}
