using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Domaine.Entities
{
    public class SaisieTemps
    {
        public int id { get; set; }
        public DateTime dateSaisie { get; set; }
        public TimeSpan heure_deb { get; set; }
        public TimeSpan heure_fin { get; set; }
        public string commentaire { get; set; }
        public string Statut { get; set; } = "En attente";
        //relation avec tache 
        public Tache Tache { get; set; }
        public int TacheId { get; set; }
        //relation avec utilisateur chef de projet et developpeur
        public ApplicationUser Utilisateur { get; set; }
        public int UtilisateurId { get; set; }
        //relation avec validation
        public ICollection<Validation>? Validations { get; set; }

    }
}
