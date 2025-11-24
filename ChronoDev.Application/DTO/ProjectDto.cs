using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Application.DTO
{
    public class ProjectDto
    {
        public string nom { get; set; }
        public DateTime dateCreation { get; set; }
        public double dureeEstimee { get; set; }
        public DateTime dateFin { get; set; }
        public int nombre_jour { get; set; }
        public string manager {  get; set; }
    
    }
}
