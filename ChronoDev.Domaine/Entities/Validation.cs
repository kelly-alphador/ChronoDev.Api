using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Domaine.Entities
{
    public class Validation
    {
        public int id { get; set; }
        public DateTime dateValidation { get; set; }
        public string Decision { get; set; } = "En attente";
        public string commentaire { get; set; }
        // Relation avec la saisie de temps
        public int SaisieDeTempsId { get; set; }
        public SaisieTemps SaisieDeTemps { get; set; } = null!;

        // Relation avec le Manager
        public int ManagerId { get; set; }
        public ApplicationUser Manager { get; set; } = null!;
    }
}
