using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class SaisieTempsReadDto
    {
        public int Id { get; set; }
        public DateTime DateSaisie { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public TimeSpan Duree { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }
        public string TacheNom { get; set; }
        public int NombreValidations { get; set; }

        // Optionnel : format lisible de la durée
        public string DureeFormatee => $"{(int)Duree.TotalHours}h {Duree.Minutes}min";
    }
}
