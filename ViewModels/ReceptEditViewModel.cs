using Recepti.Models;

namespace Recepti.ViewModels
{
    public class ReceptEditViewModel
    {
        public IFormFile? slika { get; set; }

        public Recept? Recept { get; set; }
    }
}
