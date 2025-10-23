using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class ProjectUpdateDto
    {
        public int Id { get; set; } 
        public string Nom { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateFin { get; set; }
        public int DureeEstimee { get; set; }

    }
}
