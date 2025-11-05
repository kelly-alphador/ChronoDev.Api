using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class SaisieAddDto
    {

        [Required(ErrorMessage = "La date de saisie est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime dateSaisie { get; set; }

        [Required(ErrorMessage = "L'heure de début est obligatoire")]
        public TimeSpan heure_deb { get; set; }

        [Required(ErrorMessage = "L'heure de fin est obligatoire")]
        public TimeSpan heure_fin { get; set; }

        [MaxLength(500, ErrorMessage = "Le commentaire ne peut pas dépasser 500 caractères")]
        public string commentaire { get; set; }

        [Required(ErrorMessage = "Le statut est obligatoire")]
        [MaxLength(50, ErrorMessage = "Le statut ne peut pas dépasser 50 caractères")]
        public string Statut { get; set; } = "En attente";

        [Required(ErrorMessage = "L'identifiant de la tâche est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de la tâche doit être supérieur à 0")]
        public int TacheId { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "L'identifiant de l'utilisateur doit être supérieur à 0")]
        public int UtilisateurId { get; set; }
    }
}
