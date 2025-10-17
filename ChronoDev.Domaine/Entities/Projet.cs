using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Domaine.Entities
{
    public class Projet
    {
        public int id { get; set; }
        public string nom { get; set; }
        public DateTime dateCreation { get; set; }
        public double dureeEstimee { get; set; }
        public DateTime dateFin { get; set; }
        public ApplicationUser Manager { get; set; }
        public int ManagerId { get; set; }
        public ICollection<Tache>? Taches { get; set; }

    }
}
