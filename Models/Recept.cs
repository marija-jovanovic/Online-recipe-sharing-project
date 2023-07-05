using System.ComponentModel.DataAnnotations;

namespace Recepti.Models
{
    public class Recept
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string? Naslov { get; set; }

        [Display(Name = "Vreme na podgotovka")]
        [DataType(DataType.Time)]
        public DateTime? Vreme { get; set; }

        public string? Slika { get; set; }

        public string? Tekst { get; set; }

        [Display(Name = "Povolno za vegani")]
        public string? Vegan { get; set; }

        [Display(Name = "Povolno za deca")]
        public string? Za_deca { get; set; }

        [Display(Name = "Tip na jadenje")]
        public string? Tip { get; set; }
        public string? Kreator { get; set; }

        public ICollection<Recenzija>? Recenzii { get; set; }

        [Display(Name = "Sredna Ocena")]
        public double Prosek
        {
            get
            {
                if (Recenzii == null)
                    return 0;

                double average = 0;
                int i = 0;
                if (Recenzii != null)
                {
                    foreach (var review in Recenzii)
                    {
                        average += Convert.ToDouble(review.Ocena);
                        i++;
                    }

                    return Math.Round(average / i, 2);
                }
                return 0;
            }
        }

    }
}
