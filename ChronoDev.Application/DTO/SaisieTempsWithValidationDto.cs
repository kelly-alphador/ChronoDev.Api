using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class SaisieTempsWithValidationDto
    {
        public int Id { get; set; }
        public DateTime DateSaisie { get; set; }
        public TimeSpan HeureDeb { get; set; }
        public TimeSpan HeureFin { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }
        public string Decision { get; set; }
    }
}
