using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class SaisieTempsDto
    {
        public int SaisieId { get; set; }
        public DateTime DateSaisie { get; set; }
        public TimeSpan Heures { get; set; }
        public string NomProjet { get; set; }
        public string NomTache { get; set; }
        public string NomUtilisateur { get; set; }
        public string Prenom { get; set; }
    }
}
