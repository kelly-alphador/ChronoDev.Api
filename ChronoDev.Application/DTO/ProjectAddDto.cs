using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Application.DTO
{
    public class ProjectAddDto
    {
        [Required(ErrorMessage ="cette champs est obligatoire")]
        public string nom { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public DateTime dateCreation { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public double dureeEstimee { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public DateTime dateFin { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public int ManagerId { get; set; }
    }
}
