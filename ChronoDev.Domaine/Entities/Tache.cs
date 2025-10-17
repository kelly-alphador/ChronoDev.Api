using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Domaine.Entities
{
    public class Tache
    {
        public int id { get; set; }
        public string nom { get; set; }
        public double dureeEstimee { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public Projet Projet { get; set; }
        public int ProjetId { get; set; }
        public ICollection<SaisieTemps>? SaisiesTemps { get; set; }
    }
}
