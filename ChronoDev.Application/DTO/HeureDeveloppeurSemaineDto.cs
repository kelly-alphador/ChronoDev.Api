using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class HeureDeveloppeurSemaineDto
    {
        public int UtilisateurId { get; set; }
        public string NomDeveloppeur { get; set; }
        public int Semaine { get; set; }
        public int Annee { get; set; }
        public DateTime DateDebutSemaine { get; set; }
        public DateTime DateFinSemaine { get; set; }
        public string Duree { get; set; }
        public double TotalHeures { get; set; } 
    }
}
