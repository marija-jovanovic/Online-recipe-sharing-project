using System.ComponentModel.DataAnnotations;

namespace Recepti.Models
{
    public class Recenzija
    {
        public int Id { get; set; }

        public string? Korisnik { get; set; }

        public string? Komentar {get; set;}

        public int? Ocena { get; set;}

        public int ReceptId { get; set; }

        public Recept? Recept { get; set; }

    }
}
