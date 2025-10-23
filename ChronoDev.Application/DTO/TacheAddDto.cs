using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class TacheAddDto
    {
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public string Nom { get; set; } = string.Empty;
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public double DureeEstimee { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public DateTime DateDebut { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public DateTime DateFin { get; set; }
        [Required(ErrorMessage = "cette champs est obligatoire")]
        public int ProjetId { get; set; }
    }
}
