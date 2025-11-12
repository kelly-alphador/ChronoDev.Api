using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Application.DTO
{
    public class ValadiationDto
    {
        public DateTime dateValidation { get; set; }
        public string Decision { get; set; } = "En attente";
    
        public int SaisieDeTempsId { get; set; }

        public string commentaire { get; set; }

        public int ManagerId { get; set; }
    }
}
